using System;
using System.Collections.Generic;
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
            List<string> floor = new List<string> { "Prvi", "Drugi", "Treći" };
            comboFloor.ItemsSource = floor;
            List<string> type = new List<string> {"Soba za preglede", "Soba za odmor", "Operaciona sala", "Magacin" };
            comboRoomType.ItemsSource = type;
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {
            Floor floor = Floor.first;
            RoomType type = RoomType.examinationRoom;

            if (comboFloor.SelectedIndex == 0) floor = Floor.first;
            else if (comboFloor.SelectedIndex == 1) floor = Floor.second;
            else if (comboFloor.SelectedIndex == 2) floor = Floor.third;

            if (comboRoomType.SelectedIndex == 0) type = RoomType.examinationRoom;
            else if (comboRoomType.SelectedIndex == 1) type = RoomType.recoveryRoom;
            else if (comboRoomType.SelectedIndex == 2) type = RoomType.operatingRoom;
            else if (comboRoomType.SelectedIndex == 3) type = RoomType.storageRoom;

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
