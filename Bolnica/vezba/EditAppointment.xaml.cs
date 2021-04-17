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
    public partial class EditAppointment : Window
    {
        public Appointment App { get; set; }
        public EditAppointment(Appointment p)
        {
            InitializeComponent();

            ID.Text = p.AppointentId.ToString();
            Datum.Text = p.StartTime.ToString("dd.MM.yyyy.");
            Opis.Text = p.ApointmentDescription;
            Vreme.Text = p.StartTime.ToString("HH:mm");
            Trajanje.Text = p.DurationInMunutes.ToString();
            Lekar.Text = p.Doctor.ToString();
            if (p.Room == null)
            {
                Soba.Text = "35";
            }
            else {
                Soba.Text = p.Room.RoomNumber.ToString();
            }
            App = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int id = App.AppointentId; //Int32.Parse(ID.Text);
            DateTime datum = Datum.SelectedDate.Value.Date;
            datum.ToString("MM/dd/yyyy");
            Doctor doc = App.Doctor;
            String selectedTime = Vreme.Text;
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);


            DateTime dateTimeFinal = datum.Date.Add(dateTime.TimeOfDay);

            PatientStorage pps = new PatientStorage();
            Patient patient = pps.GetOne("1008985563244");

            Appointment pat = new Appointment(doc, dateTimeFinal, patient);
            pat.AppointentId = id;
            AppointmentStorage ps = new AppointmentStorage();
            ps.Update(pat);

            var pa = ChangeAppointmentView.Appointments.FirstOrDefault(p => p.AppointentId.Equals(id));
            if (pa != null)
            {
                ChangeAppointmentView.Appointments[ChangeAppointmentView.Appointments.IndexOf(pa)] = pat;
            }

            this.Close();
        }
    }
}
