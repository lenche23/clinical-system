using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace vezba
{
    public partial class NewAppointment : Window
    {
        public NewAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> patients = ps.GetAll();
            Patient.ItemsSource = patients;
            DoctorFileRepository ds = new DoctorFileRepository();
            List<Doctor> doctors = ds.GetAll();
            Doctor.ItemsSource = doctors;
            RoomFileRepository rs = new RoomFileRepository();
            List<Room> rooms = rs.GetAll();
            Room.ItemsSource = rooms;
            List<int> durations = new List<int> { 15, 30, 45, 60 };
            Duration.ItemsSource = durations;
            List<string> hours = new List<string> {"08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"};
            Hours.ItemsSource = hours;
            List<string> minutes = new List<string> {"00", "15", "30", "45"};
            Minutes.ItemsSource = minutes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateEntries() == false)
                return;

            Patient patient = (Patient)Patient.SelectedItem;
            Doctor doctor = (Doctor)Doctor.SelectedItem;
            Room room = (Room)Room.SelectedItem;
            DateTime startTime = GetTime();
            int duration = (int)Duration.SelectedItem;
            string apDesc = Description.Text;

            AppointmentFileRepository aps = new AppointmentFileRepository();
            int id = aps.GenerateNextId();

            Appointment appointment = new Appointment(id, patient, doctor, room, startTime, duration, apDesc);

            if (GetOverlapingAppoinments(appointment).Count == 0)
            {
                Boolean b = aps.Save(appointment);
                if (b)
                {
                    SecretaryAppointments.Appointments.Add(appointment);
                }
                this.Close();
            }
            else
            {
                DateTime recommendedTime = FindNextFreeAppointment(appointment);
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.\n Prvi sledeci dostupan termin za unete kriterijume je " + recommendedTime.ToString("dd.MM.yyyy. HH:mm"));
            }

            

        }
        
        private Boolean ValidateEntries()
        {
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli pacijenta!");
                return false;
            }
            if (Doctor.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli lekara!");
                return false;
            }
            if (Room.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli prostoriju!");
                return false;
            }
            if (Hours.SelectedItem == null || Minutes.SelectedItem == null)
            {
                MessageBox.Show("Niste izabrali vreme!");
                return false;
            }
            if (DateTime.Compare(GetTime(), DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je vec prosao!");
                return false;
            }
            if (Duration.SelectedItem == null)
            {
                MessageBox.Show("Niste izabrali trajanje termina!");
                return false;
            }
            return true;
        }

        private DateTime GetTime()
        {
            DateTime selectedDate = new DateTime(1900, 1, 1);
            try
            {
                selectedDate = Date.SelectedDate.Value.Date;
            }
            catch
            {
                MessageBox.Show("Niste izabrali datum!");
                return selectedDate;
            }
            string hours = (string) Hours.SelectedItem;
            string minutes = (string) Minutes.SelectedItem;
            String selectedTime = (Convert.ToString(hours) + ":" + Convert.ToString(minutes)).Trim();
            DateTime time;
            DateTime.TryParseExact(selectedTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out time);
            DateTime startTime = selectedDate.Date.Add(time.TimeOfDay);
            return startTime;
        }

        private List<Appointment> GetOverlapingAppoinments(Appointment appointment)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();
            List<Appointment> overlaping = new List<Appointment>();
            for (int i = 0; i < appointments.Count; i++)
            {
                if(this.DoShareResources(appointment, appointments[i]))
                {
                    overlaping.Add(appointments[i]);
                }
            }
            return overlaping;
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

        private DateTime FindNextFreeAppointment(Appointment appointment)
        {
            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in appointments)
                {
                    if(DoShareResources(a, appointment))
                    {
                        appointment.StartTime = generateNewStartTime(a.EndTime);//.StartTime.AddMinutes(a.DurationInMunutes)); // dodati metodu get end time u klasu appointment
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
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
    }
}
