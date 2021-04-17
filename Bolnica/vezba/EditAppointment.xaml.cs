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
        public EditAppointment(Appointment p)
        {
            InitializeComponent();
            
            ID.Text = p.AppointentId.ToString();
            Datum.Text = p.StartTime.ToString("dd.MM.yyyy.");
            Opis.Text = p.ApointmentDescription;
            Vreme.Text = p.StartTime.ToString("HH:mm");
            Trajanje.Text = p.DurationInMunutes.ToString();
            //Soba.Text = p.Room.RoomNumber.ToString();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int id = Int32.Parse(ID.Text);
            DateTime datum = Datum.SelectedDate.Value.Date;
            datum.ToString("MM/dd/yyyy");

            //Doctor doc = (Doctor)Lekar.Text;
            Doctor doc = new Doctor();
            doc.Name = "dr BLABLABALBA";
            String selectedTime = Vreme.Text;
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);


            DateTime dateTimeFinal = datum.Date.Add(dateTime.TimeOfDay);


            Appointment pat = new Appointment(doc, dateTimeFinal);
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
