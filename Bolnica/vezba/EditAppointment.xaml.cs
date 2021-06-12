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
            if(ValidateTime() == false)
            {
                MessageBox.Show("Izabrani datum je vec prosao!");
                return;
            }

            AppointmentFileRepository aps = new AppointmentFileRepository();
            Appointment appo = new Appointment(appointment.AppointentId, appointment.Patient, appointment.Doctor, appointment.Room, startTime, appointment.DurationInMunutes, appointment.ApointmentDescription, null);
            

            if (GetOverlapingAppoinments(appointment).Count == 0)
            {
                aps.Update(this.appointment);
                var ap = SecretaryAppointments.Appointments.FirstOrDefault(a => a.AppointentId.Equals(this.appointment.AppointentId));
                if (ap != null)
                {
                    SecretaryAppointments.Appointments[SecretaryAppointments.Appointments.IndexOf(ap)] = appo;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.");
            }

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
            if (a1.Doctor.Jmbg.Equals(a2.Doctor.Jmbg) || a1.Patient.Jmbg.Equals(a2.Patient.Jmbg) || a1.Room.RoomNumber == a2.Room.RoomNumber)
            {
                DateTime beginning1 = a1.StartTime;
                DateTime end1 = a1.StartTime.AddMinutes(a1.DurationInMunutes);
                DateTime beginning2 = a2.StartTime;
                DateTime end2 = a2.StartTime.AddMinutes(a2.DurationInMunutes);
                if (DateTime.Compare(end2, beginning1) <= 0) //drugi zavrsava pre pocetka prvog
                {
                    return false;
                }
                else if (DateTime.Compare(end1, beginning2) <= 0) //prvi zavrsava pre pocetka drugog
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;

        }
    }
}
