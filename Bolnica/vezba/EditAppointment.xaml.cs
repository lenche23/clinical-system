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
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        private Appointment appointment;
        public EditAppointment(Appointment a)
        {
            InitializeComponent();
            appointment = a;
            Patient.Content = appointment.PatientName;
            Doctor.Content = appointment.DoctorName;
            Room.Content = appointment.RoomName;
            Duration.Content = appointment.DurationInMunutes;
            Duration.Content += " minuta";
            Description.Text = appointment.ApointmentDescription;

            List<string> hours = new List<string> { "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            Hours.ItemsSource = hours;
            string h = appointment.StartTime.ToString("HH");
            Hours.SelectedItem = h;
            List<string> minutes = new List<string> { "00", "15", "30", "45" };
            Minutes.ItemsSource = minutes;
            string m = appointment.StartTime.ToString("mm");
            Minutes.SelectedItem = m;
            Date.SelectedDate = appointment.StartTime;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            DateTime startTime = GetTime();
            this.appointment.StartTime = startTime;

            AppointmentStorage aps = new AppointmentStorage();
            aps.Update(this.appointment);
            Appointment appo = new Appointment(appointment.AppointentId, appointment.Patient, appointment.Doctor, appointment.Room, startTime, appointment.DurationInMunutes, appointment.ApointmentDescription);
            
            var ap = SecretaryAppointments.Appointments.FirstOrDefault(a => a.AppointentId.Equals(this.appointment.AppointentId));
            if (ap != null)
            {
                SecretaryAppointments.Appointments[SecretaryAppointments.Appointments.IndexOf(ap)] = appo;
            }
            this.Close();

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
