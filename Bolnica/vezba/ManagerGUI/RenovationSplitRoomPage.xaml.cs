using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RenovationSplitRoomPage : Page
    {
        private Room selected;
        private MainManagerWindow mainManagerWindow;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        public RenovationSplitRoomPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            this.selected = selected;
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

            RoomType type1 = RoomType.examinationRoom;

            if (Convert.ToBoolean(Pregled1.IsChecked))
            {
                type1 = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija1.IsChecked))
            {
                type1 = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor1.IsChecked))
            {
                type1 = RoomType.recoveryRoom;
            }

            else if (Convert.ToBoolean(Magacin1.IsChecked))
            {
                type1 = RoomType.storageRoom;
            }

            RoomType type2 = RoomType.examinationRoom;

            if (Convert.ToBoolean(Pregled2.IsChecked))
            {
                type2 = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija2.IsChecked))
            {
                type2 = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor2.IsChecked))
            {
                type2 = RoomType.recoveryRoom;
            }

            else if (Convert.ToBoolean(Magacin2.IsChecked))
            {
                type2 = RoomType.storageRoom;
            }

            Floor floor = selected.RoomFloor;
            RoomService roomService = new RoomService();
            var newRoom1 = new Room(endTime, 0, floor, type1);
            var newRoom2 = new Room(endTime,0, floor, type2);

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

            var newRenovation = new Renovation(startTime, durationInDays, id);
            selected.AddRenovation(newRenovation);
            selected.EndDateTime = endTime;
            roomService.UpdateRoom(selected);
            RenovationsPage.RenovationList.Add(newRenovation);
            roomService.SaveRoom(newRoom1);
            roomService.SaveRoom(newRoom2);
            NavigationService.GoBack();
        }
    }
}
