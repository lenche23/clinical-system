using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vezba.SecretaryGUI
{
    public partial class SecretaryOverlapingAppointments : Window
    {
        public ObservableCollection<AppointmentForReschedulingDTO> Appointments { get; set; }
        public SecretaryOverlapingAppointments(Appointment emergencyAppointment)
        {
            InitializeComponent(); 
            this.DataContext = this;
            Appointments = new ObservableCollection<AppointmentForReschedulingDTO>();

            Patient.Content = emergencyAppointment.PatientName;
            Speciality.Content = emergencyAppointment.Doctor.Speciality;
            Room.Content = emergencyAppointment.RoomName;
            Duration.Content = emergencyAppointment.DurationInMunutes;

            emergencyAppointment.StartTime = DateTime.Now;
            List<Appointment> overlaping = GetEmergencyOverlapingAppointments(emergencyAppointment);
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = TransformAppointmentsForRescheduling(overlaping, emergencyAppointment);// overlaping);
            foreach (AppointmentForReschedulingDTO ad in appointmentsForRescheduling)
                Appointments.Add(ad);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void RescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            //IMENA
            if (overlapingAppointmentsTable.SelectedCells.Count > 0)
            {
                AppointmentForReschedulingDTO ad = (AppointmentForReschedulingDTO)overlapingAppointmentsTable.SelectedItem;
                AppointmentFileRepository aps = new AppointmentFileRepository();
                List<Appointment> scheduledAppointments = aps.GetAll();
                Appointment rescheduledAppointment = scheduledAppointments.FirstOrDefault(a => a.AppointentId.Equals(ad.AppointmentId));
                if (rescheduledAppointment != null)
                {
                    rescheduledAppointment.StartTime = ad.SuggestedTime;
                    aps.Update(rescheduledAppointment);
                    Appointment api = SecretaryAppointments.Appointments.FirstOrDefault(a => a.AppointentId.Equals(rescheduledAppointment.AppointentId));
                    if (api != null)
                        SecretaryAppointments.Appointments[SecretaryAppointments.Appointments.IndexOf(api)] = rescheduledAppointment;
                    Appointments.Remove(ad);
                }
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        private List<AppointmentForReschedulingDTO> TransformAppointmentsForRescheduling(List<Appointment> overlapingAppointments, Appointment emergencyAppointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            scheduledAppointments.Add(emergencyAppointment);
            //-------------------------------------------------------------S remove list from list
            foreach (Appointment oAppointment in overlapingAppointments)
            {
                var appointment = scheduledAppointments.FirstOrDefault(a => a.AppointentId.Equals(oAppointment.AppointentId));
                if (appointment != null)
                    scheduledAppointments.Remove(appointment);
            }
            //-------------------------------------------------------------E
            overlapingAppointments = SortAppointmentsByStartTime(overlapingAppointments);

            //-------------------------------------------------------------S transform
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = new List<AppointmentForReschedulingDTO>();
            for (int i = 0; i < overlapingAppointments.Count; i++)
            {
                AppointmentForReschedulingDTO ad = new AppointmentForReschedulingDTO(overlapingAppointments[i]);
                ad.SuggestedTime = FindNextFreeAppointmentStartTimeInAppointmentList(overlapingAppointments[i], scheduledAppointments);
                overlapingAppointments[i].StartTime = ad.SuggestedTime;
                scheduledAppointments.Add(overlapingAppointments[i]);
                appointmentsForRescheduling.Add(ad);
            }
            //-------------------------------------------------------------E

            return appointmentsForRescheduling;
        }

        private List<Appointment> SortAppointmentsByStartTime(List<Appointment> appointments)
        {
            appointments.Sort((a1, a2) => a1.StartTime.CompareTo(a2.StartTime));
            return appointments;
        }

        private List<Appointment> GetEmergencyOverlapingAppointments(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            List<Appointment> overlapingAppointments = new List<Appointment>();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (this.EmergencyAppointmentsOverlap(appointment, scheduledAppointments[i]))
                {
                    overlapingAppointments.Add(scheduledAppointments[i]);
                }
            }
            return overlapingAppointments;
        }

        private Boolean EmergencyAppointmentsOverlap(Appointment appointment1, Appointment appointment2)
        {
            if (AppointmentsShareDoctorSpeciality(appointment1, appointment2) || AppointmentsSharePatient(appointment1, appointment2) || AppointmentsShareRoom(appointment1, appointment2))
            {
                if (DateTime.Compare(appointment2.EndTime, appointment1.StartTime) <= 0) //drugi zavrsava pre pocetka prvog
                    return false;
                else if (DateTime.Compare(appointment1.EndTime, appointment2.StartTime) <= 0) //prvi zavrsava pre pocetka drugog
                    return false;
                else
                    return true;
            }
            return false;

        }

        private Boolean AppointmentsSharePatient(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Patient.Jmbg.Equals(appointment2.Patient.Jmbg);
        }

        private Boolean AppointmentsShareDoctorSpeciality(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Doctor.Speciality.Name.Equals(appointment2.Doctor.Speciality.Name);
        }

        private Boolean AppointmentsShareRoom(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Room.RoomNumber == appointment2.Room.RoomNumber;
        }

        private DateTime FindNextFreeAppointmentStartTimeInAppointmentList(Appointment appointment, List<Appointment> scheduledAppointments)
        {
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (EmergencyAppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }

        private DateTime CalculateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime.AddMinutes(5);
            if (IsAfterWorkingHours(newStartTime))
            {
                newStartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, 8, 0, 0);
                newStartTime = newStartTime.AddDays(1);
            }
            return newStartTime;
        }

        private Boolean IsAfterWorkingHours(DateTime time)
        {
            DateTime endOfDay = new DateTime(time.Year, time.Month, time.Day, 19, 45, 0);
            if (time >= endOfDay)
                return true;

            return false;
        }
    }
}
