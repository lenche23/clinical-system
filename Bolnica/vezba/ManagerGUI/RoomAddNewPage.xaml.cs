using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomAddNewPage : Page
    {
        public RoomAddNewPage()
        {
            InitializeComponent();
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {

            Floor floor = Floor.first;
            RoomType type = RoomType.examinationRoom;

            if (Convert.ToBoolean(Prvi.IsChecked))
            {
                floor = Floor.first;
            }

            else if (Convert.ToBoolean(Drugi.IsChecked))
            {
                floor = Floor.second;
            }

            else if (Convert.ToBoolean(Treci.IsChecked))
            {
                floor = Floor.third;
            }


            if (Convert.ToBoolean(Pregled.IsChecked))
            {
                type = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija.IsChecked))
            {
                type = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor.IsChecked))
            {
                type = RoomType.recoveryRoom;
            }

            RoomService roomService = new RoomService();
            var BrojSobe = int.Parse(BrojSobeTB.Text);
            var newRoom = new Room(DateTime.Now, BrojSobe, floor, type);
            roomService.SaveRoom(newRoom);
            RoomsPage.Rooms.Add(newRoom);
            NavigationService.GoBack();
        }

        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
