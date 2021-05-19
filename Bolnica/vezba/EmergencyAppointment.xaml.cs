using Bolnica;
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

namespace vezba
{
    /// <summary>
    /// Interaction logic for EmergencyAppointment.xaml
    /// </summary>
    public partial class EmergencyAppointment : Window
    {
        public EmergencyAppointment()
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

        private void Schedule_Button_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)Patient.SelectedItem;
            Speciality speciality = (Speciality)Speciality.SelectedItem;
            Room room = (Room)Room.SelectedItem;
            int duration = (int)Duration.SelectedItem;
            string description = Description.Text;

            Appointment emergencyAppointment = MakeEmergencyAppointment(patient, speciality, room, duration, description);
            if (emergencyAppointment != null)
            {
                DateTime timeLimit = DateTime.Now.AddMinutes(15);
                if(emergencyAppointment.StartTime <= timeLimit)
                {
                    AppointmentFileRepository aps = new AppointmentFileRepository();
                    Boolean b = aps.Save(emergencyAppointment);
                    if (b)
                    {
                        SecretaryAppointments.Appointments.Add(emergencyAppointment);
                        MessageBox.Show("Zakazan je hitan termin za " + emergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + emergencyAppointment.DoctorName);
                    }
                    this.Close();
                }
                else
                {
                    var s = new ScheduleEmergencyAppointment(emergencyAppointment);
                    s.Show();
                }

                

            }
            return;
        }

        private Appointment MakeEmergencyAppointment(Patient patient, Speciality speciality, Room room, int duration, string description)
        {
            DoctorFileRepository ds = new DoctorFileRepository();
            List<Doctor> doctors = ds.GetDoctorsWithSpeciality(speciality);
            if (doctors.Count == 0)
            {
                MessageBox.Show("Nema doktora sa ovom specijalizacijom!"); 
                return null;
            }
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = new List<Appointment>();
            foreach(Doctor d in doctors)
            {
                Appointment emergencyAppointment = new Appointment(aps.generateNextId(), patient, d, room, DateTime.Now, duration, description);
                emergencyAppointment = FindFirstFreeAppointment(emergencyAppointment);
                    appointments.Add(emergencyAppointment);
            }
            return FindEarliestAppointment(appointments);
        }

        private Appointment FindEarliestAppointment(List<Appointment> appointments)
        {
            Appointment earliestAppoinment = appointments[0];
            foreach(Appointment a in appointments)
            {
                if(a.StartTime < earliestAppoinment.StartTime)
                    earliestAppoinment = a;
            }
            return earliestAppoinment;
        }

        private Boolean DoShareResources(Appointment a1, Appointment a2)
        {
            if (a1.Doctor.Jmbg.Equals(a2.Doctor.Jmbg) || a1.Patient.Jmbg.Equals(a2.Patient.Jmbg) || a1.Room.RoomNumber == a2.Room.RoomNumber)
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

        private Appointment FindFirstFreeAppointment(Appointment appointment)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();
            //DateTime startTimeLimit = appointment.StartTime.AddHours(1);
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in appointments)
                {
                    if (DoShareResources(a, appointment))
                    {
                        appointment.StartTime = generateNewStartTime(a.EndTime);//.StartTime.AddMinutes(a.DurationInMunutes)); // dodati metodu get end time u klasu appointment
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment;
        }

        private DateTime generateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime;
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

        private void Register_Patient_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new NewPatient();
            s.Show();
        }
    }
}
