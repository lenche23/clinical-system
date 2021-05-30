using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Service;

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
            AppointmentService appointmentService = new AppointmentService();

            if (DateTime.Compare(startTime, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je već prošao!");
                return;
            }

            if (appointmentService.RenovationAppointmentOverlapping(startTime, endTime, selected))
            {
                MessageBox.Show("Datum renovacije se poklapa sa već zakazanim pregledima");
                return;
            }
            var newRenovation = new Renovation(startTime, durationInDays, id);
            selected.AddRenovation(newRenovation);
            RoomService roomService = new RoomService();
            roomService.UpdateRoom(this.selected);
            WindowRenovations.RenovationList.Add(newRenovation);
            NavigationService.GoBack();

        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
