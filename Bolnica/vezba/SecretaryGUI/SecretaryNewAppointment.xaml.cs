using Model;
using Service;
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

namespace vezba.SecretaryGUI
{
    public partial class SecretaryNewAppointment : Window
    {
        public SecretaryNewAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientService ps = new PatientService();
            List<Patient> patients = ps.GetAllPatients();
            Patient.ItemsSource = patients;
            DoctorService ds = new DoctorService();
            List<Doctor> doctors = ds.GetAllDoctors();
            Doctor.ItemsSource = doctors;
            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRooms();
            Room.ItemsSource = rooms;
            List<int> durations = new List<int> { 15, 30, 45, 60 };
            Duration.ItemsSource = durations;
            List<string> hours = new List<string> { "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            Hours.ItemsSource = hours;
            List<string> minutes = new List<string> { "00", "15", "30", "45" };
            Minutes.ItemsSource = minutes;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEntries() == false)
                return;
            AppointmentService appointmentService = new AppointmentService();
            Appointment newAppointment = new Appointment(0, (Patient)Patient.SelectedItem, (Doctor)Doctor.SelectedItem, (Room)Room.SelectedItem, GetTime(), (int)Duration.SelectedItem, Description.Text, null);
            Boolean isSuccess = appointmentService.ScheduleAppointment(newAppointment);
            if (isSuccess)
            {
                SecretaryAppointments.Appointments.Add(newAppointment);
                this.Close();
            }
                
        }

        //HCI deo
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
            string hours = (string)Hours.SelectedItem;
            string minutes = (string)Minutes.SelectedItem;
            String selectedTime = (Convert.ToString(hours) + ":" + Convert.ToString(minutes)).Trim();
            DateTime time;
            DateTime.TryParseExact(selectedTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out time);
            DateTime startTime = selectedDate.Date.Add(time.TimeOfDay);
            return startTime;
        }
    }
}
