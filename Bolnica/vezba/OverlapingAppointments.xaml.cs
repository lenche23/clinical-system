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

namespace vezba
{
    
    public partial class OverlapingAppointments : Window
    {
        public ObservableCollection<AppointmentForReschedulingDTO> Appointments { get; set; }
        
        public OverlapingAppointments(Appointment emergencyAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentFileRepository s = new AppointmentFileRepository();
            List<Appointment> temp = s.GetAll();
            Appointments = new ObservableCollection<AppointmentForReschedulingDTO>();
            
            emergencyAppointment.StartTime = DateTime.Now;
            List<Appointment> overlaping = GetOverlapingAppoinments(emergencyAppointment);
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = TransformAppointmentsForRescheduling(overlaping, emergencyAppointment);// overlaping);
            foreach (AppointmentForReschedulingDTO ad in appointmentsForRescheduling)
                Appointments.Add(ad);
        }

        private List<AppointmentForReschedulingDTO> TransformAppointmentsForRescheduling(List<Appointment> overlapingAppointments, Appointment emergencyAppointment)
        {
            AppointmentFileRepository apFileRepository = new AppointmentFileRepository();
            List<Appointment> allScheduledAppoinments = apFileRepository.GetAll();
            allScheduledAppoinments.Add(emergencyAppointment);
            foreach(Appointment oAppointment in overlapingAppointments)
            {
                var appointment = allScheduledAppoinments.FirstOrDefault(a => a.AppointentId.Equals(oAppointment.AppointentId));
                if (appointment != null)
                    allScheduledAppoinments.Remove(appointment);
            }
            overlapingAppointments = SortAppointmentsByStartTime(overlapingAppointments);
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = new List<AppointmentForReschedulingDTO>();
            for(int i = 0; i < overlapingAppointments.Count; i++)
            {
                AppointmentForReschedulingDTO ad = new AppointmentForReschedulingDTO(overlapingAppointments[i]);
                ad.SuggestedTime = FindNextFreeAppointmentTime(overlapingAppointments[i], allScheduledAppoinments);
                overlapingAppointments[i].StartTime = ad.SuggestedTime;
                allScheduledAppoinments.Add(overlapingAppointments[i]);
                appointmentsForRescheduling.Add(ad);
            }

            return appointmentsForRescheduling;
        }

        private List<Appointment> SortAppointmentsByStartTime(List<Appointment> appointments)
        {
            appointments.Sort((a1, a2) => a1.StartTime.CompareTo(a2.StartTime));
            return appointments;
        }

        private List<Appointment> GetOverlapingAppoinments(Appointment appointment)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();
            List<Appointment> overlaping = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (this.DoShareResources(appointment, appointments[i]))
                {
                    overlaping.Add(appointments[i]);
                }
            }
            return overlaping;
        }

        private Boolean DoShareResources(Appointment a1, Appointment a2)
        {
            if (a1.Doctor.Speciality.Name.Equals(a2.Doctor.Speciality.Name) || a1.Patient.Jmbg.Equals(a2.Patient.Jmbg) || a1.Room.RoomNumber == a2.Room.RoomNumber)
            {
                if (DateTime.Compare(a2.EndTime, a1.StartTime) <= 0) //drugi zavrsava pre pocetka prvog
                    return false;
                else if (DateTime.Compare(a1.EndTime, a2.StartTime) <= 0) //prvi zavrsava pre pocetka drugog
                    return false;
                else
                    return true;
            }
            return false;

        }

        private DateTime FindNextFreeAppointmentTime(Appointment appointment, List<Appointment> scheduledAppointments)
        {
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (DoShareResources(a, appointment))
                    {
                        appointment.StartTime = GenerateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }

        private DateTime GenerateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime.AddMinutes(5);
            if (isAfterHours(newStartTime))
            {
                newStartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, 8, 0, 0);
                newStartTime = newStartTime.AddDays(1);
            }
            return newStartTime;
        }

        private Boolean isAfterHours(DateTime time)
        {
            DateTime endOfDay = new DateTime(time.Year, time.Month, time.Day, 19, 45, 0);
            if (time >= endOfDay)
                return true;

            return false;
        }

        private void Postpone_Button_Click(object sender, RoutedEventArgs e)
        {
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

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
