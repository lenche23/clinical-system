using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RenovationViewPage : Page
    {
        private Room selected;
        private MainManagerWindow mainManagerWindow;
        public RenovationViewPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
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
            List<Appointment> appointments = appointmentService.GetAllAppointments();

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
            RoomService roomService = new RoomService();
            roomService.UpdateRoom(this.selected);
            RenovationsPage.RenovationList.Add(newRenovation);
            NavigationService.GoBack();
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
            NavigationService.GoBack();
        }
    }
}
