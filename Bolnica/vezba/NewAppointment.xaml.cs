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

namespace vezba
{
    /// <summary>
    /// Interaction logic for NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Window
    {
        public NewAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientStorage ps = new PatientStorage();
            List<Patient> patients = ps.GetAll();
            Patient.ItemsSource = patients;
            DoctorStorage ds = new DoctorStorage();
            List<Doctor> doctors = ds.GetAll();
            Doctor.ItemsSource = doctors;
            RoomStorage rs = new RoomStorage();
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

            AppointmentStorage aps = new AppointmentStorage();
            int id = aps.generateNextId();

            Appointment appointment = new Appointment(id, patient, doctor, room, startTime, duration, apDesc);

            Boolean b = aps.Save(appointment);
            if (b)
            {
                SecretaryAppointments.Appointments.Add(appointment);
            }
            this.Close();

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

        private Boolean ValidateTime()
        {
            if (DateTime.Compare(GetTime(), DateTime.Now) < 0)
                return false;
            else
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
            //MessageBox.Show(hours);
            String selectedTime = (Convert.ToString(hours) + ":" + Convert.ToString(minutes)).Trim();
            DateTime time;
            DateTime.TryParseExact(selectedTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out time);
            DateTime startTime = selectedDate.Date.Add(time.TimeOfDay);
            return startTime;
        }

        
    }
}
