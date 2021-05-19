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

namespace vezba.ManagerGUI
{
    public partial class RoomNewRenovationPage : Page
        {
        private Room selected;
        public RoomNewRenovationPage(Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            BrojProstorije.Content = BrojProstorije.Content + " " + selected.RoomNumber;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var format = "dd/MM/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var startTime = DateTime.ParseExact(PocetniDatum.Text, format, provider);
            var durationInDays = int.Parse(Trajanje.Text);
            var endTime = startTime.AddDays(durationInDays);
            var id = selected.renovation.Count + 1;

            AppointmentFileRepository aps = new AppointmentFileRepository();
            List<Appointment> appointments = aps.GetAll();

            if (DateTime.Compare(startTime, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je već prošao!");
                return;
            }

            if (Overlap(appointments, startTime, endTime))
            {
                MessageBox.Show("Datum renovacije se poklapa sa već zakazanim pregledima");
                return;
            }

            var newRenovation = new Renovation(startTime, durationInDays, id);
            selected.AddRenovation(newRenovation);
            RoomFileRepository rs = new RoomFileRepository();
            rs.Update(this.selected);
            WindowRenovations.RenovationList.Add(newRenovation);
            //this.Close();

        }

        private bool Overlap(List<Appointment> appointments, DateTime StartTime, DateTime EndTime)
        {
            var overlap = false;

            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].Room.RoomNumber == selected.RoomNumber)
                {
                    DateTime appointmentStart = appointments[i].StartTime;
                    if (DateTime.Compare(appointmentStart, StartTime) > 0 && DateTime.Compare(appointmentStart, EndTime) < 0)
                    {
                        overlap = true;
                    }
                }
            }

            return overlap;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
    }
}
