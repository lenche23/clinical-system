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
    public partial class PatientEditAppointment : Window
    {
        public Appointment App { get; set; }
        public PatientEditAppointment(Appointment appointment)
        {
            InitializeComponent();
            ID.Text = appointment.AppointentId.ToString();
            Datum.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            Opis.Text = appointment.ApointmentDescription;
            Vreme.Text = appointment.StartTime.ToString("HH:mm");
            Trajanje.Text = appointment.DurationInMunutes.ToString();
            Lekar.Text = appointment.Doctor.ToString();
            if (appointment.Room == null)
            {
                Soba.Text = "100";
            }
            else
            {
                Soba.Text = appointment.Room.RoomNumber.ToString();
            }
            App = appointment;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime initDate = App.StartTime.Date;
            DateTime endDate = Datum.SelectedDate.Value.Date;
            int diff = (endDate - initDate).Days;
            if (diff > 2)
            {
                MessageBox.Show("Pregled mozete pomeriti maksimalno za dva dana.");
            }

            int id = App.AppointentId;
            DateTime date = Datum.SelectedDate.Value.Date;
            date.ToString("MM/dd/yyyy");
            Doctor initDoctor = App.Doctor;
            String selectedTime = Vreme.Text;
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime dateTimeFinal = date.Date.Add(dateTime.TimeOfDay);

            PatientStorage patientStorage = new PatientStorage();
            Patient patient = patientStorage.GetOne("1008985563244");

            Appointment appointment = new Appointment(initDoctor, dateTimeFinal, patient);
            appointment.AppointentId = id;
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointmentStorage.Update(appointment);

            var appointment1 = ChangeAppointmentView.Appointments.FirstOrDefault(a => a.AppointentId.Equals(id));
            if (appointment1 != null)
            {
                ChangeAppointmentView.Appointments[ChangeAppointmentView.Appointments.IndexOf(appointment1)] = appointment;
            }

            this.Close();
        }
    }
}
