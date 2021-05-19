using Model;
using System;
using System.Collections.Generic;
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
using vezba.Repository;

namespace vezba.SecretaryGUI
{
    public partial class SecretaryNewEmergencyAppointment : Window
    {
        public SecretaryNewEmergencyAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> patients = ps.GetAll();
            Patient.ItemsSource = patients;
            List<Speciality> specialities = new List<Speciality>();
            specialities.Add(new Speciality("Kardiolog"));
            specialities.Add(new Speciality("Opsti"));
            specialities.Add(new Speciality("Stomatolog"));
            specialities.Add(new Speciality("Oftalmolog"));
            Speciality.ItemsSource = specialities;
            RoomFileRepository rs = new RoomFileRepository();
            List<Room> rooms = rs.GetAll();
            Room.ItemsSource = rooms;
            List<int> durations = new List<int> { 15, 30, 45, 60 };
            Duration.ItemsSource = durations;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
        private void RegisterPatientButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewPatient s = new SecretaryNewPatient();
            s.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)Patient.SelectedItem;
            Speciality speciality = (Speciality)Speciality.SelectedItem;
            Room room = (Room)Room.SelectedItem;
            int duration = (int)Duration.SelectedItem;
            string description = Description.Text;

            Appointment emergencyAppointment = MakeEarliestEmergencyAppointment(patient, speciality, room, duration, description);
            if (emergencyAppointment == null)
                return;
            DateTime timeLimit = DateTime.Now.AddMinutes(15);
            if (emergencyAppointment.StartTime <= timeLimit)
            {
                AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
                Boolean isSuccess = appointmentFileRepository.Save(emergencyAppointment);
                if (isSuccess)
                {
                    SecretaryAppointments.Appointments.Add(emergencyAppointment);
                    MessageBox.Show("Zakazan je hitan termin za " + emergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + emergencyAppointment.DoctorName);
                }
                this.Close();
            }
            else
            {
                SecretaryNearestAvailableEmergencyAppointment s = new SecretaryNearestAvailableEmergencyAppointment(emergencyAppointment);
                s.Show();
            }
            return;
        }

        private Appointment MakeEarliestEmergencyAppointment(Patient patient, Speciality speciality, Room room, int duration, string description)
        {
            DoctorFileRepository doctorFileRepository = new DoctorFileRepository();
            List<Doctor> doctors = doctorFileRepository.GetDoctorsWithSpeciality(speciality); // izmeniti tako da postoji funkcija koja vraca postojece specijalizacije
            if (doctors.Count == 0)
            {
                MessageBox.Show("Nema doktora sa ovom specijalizacijom!");
                return null;
            }

            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in doctors)
            {
                Appointment emergencyAppointment = new Appointment(appointmentFileRepository.generateNextId(), patient, d, room, DateTime.Now, duration, description);
                //emergencyAppointment = FindFirstFreeAppointment(emergencyAppointment);
                emergencyAppointment.StartTime = FindNextFreeAppointmentStartTime(emergencyAppointment);
                appointments.Add(emergencyAppointment);
            }
            return FindEarliestAppointment(appointments);
        }

        private Appointment FindEarliestAppointment(List<Appointment> appointments)
        {
            Appointment earliestAppoinment = appointments[0];
            foreach (Appointment a in appointments)
            {
                if (a.StartTime < earliestAppoinment.StartTime)
                    earliestAppoinment = a;
            }
            return earliestAppoinment;
        }

        private Boolean AppointmentsOverlap(Appointment appointment1, Appointment appointment2)
        {
            if (AppointmentsShareDoctor(appointment1, appointment2) || AppointmentsSharePatient(appointment1, appointment2) || AppointmentsShareRoom(appointment1, appointment2))
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

        private Boolean AppointmentsShareDoctor(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Doctor.Jmbg.Equals(appointment2.Doctor.Jmbg);
        }

        private Boolean AppointmentsShareRoom(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Room.RoomNumber == appointment2.Room.RoomNumber;
        }

        private DateTime FindNextFreeAppointmentStartTime(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (AppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }

        private Appointment FindFirstFreeAppointment(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (AppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment;
        }

        private DateTime CalculateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime;
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
