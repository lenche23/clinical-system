using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RenovationMergeRoomsPage : Page
    {
        private MainManagerWindow mainManagerWindow;
        public List<Room> roomList { get; set; }
        private Room selected;
        public RenovationMergeRoomsPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            this.DataContext = selected;
            this.mainManagerWindow = mainManagerWindow;
            this.selected = selected;

            RoomService roomService = new RoomService();
            roomList = roomService.GetAllRooms();
            RoomToMerge.ItemsSource = roomList;
            BrojProstorije.Content = BrojProstorije.Content + " " + selected.RoomNumber;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

            if (appointmentService.Overlap(selected, startTime, endTime))
            {
                MessageBox.Show("Datum renovacije se poklapa sa već zakazanim pregledima");
                return;
            }

            Room roomToMerge = (Room)RoomToMerge.SelectedItem;
            var newRenovation = new Renovation(startTime, durationInDays, id);
            roomToMerge.AddRenovation(newRenovation);
            selected.AddRenovation(newRenovation);
            roomToMerge.EndDateTime = endTime;
            RoomService roomService = new RoomService();
            roomService.UpdateRoom(roomToMerge);
            roomService.UpdateRoom(selected);
            RenovationsPage.RenovationList.Add(newRenovation);

            NavigationService.GoBack();
        }
    }
}
