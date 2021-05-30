using System;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomsPage : Page
    {
        public static ObservableCollection<Room> Rooms { get; set; }
        public static List<Room> rooms;
        private MainManagerWindow mainManagerWindow;
        public RoomsPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            RoomService roomService = new RoomService();
            rooms = roomService.GetAllRooms();
            Rooms = new ObservableCollection<Room>();

            foreach (Room room in rooms)
            {
                if (DateTime.Compare(room.StartDateTime, DateTime.Now) <= 0 && DateTime.Compare(room.EndDateTime, DateTime.Now) >= 0)
                {
                    Rooms.Add(room);
                }
            }
            lvDataBinding.ItemsSource = Rooms;
        }

        private void New_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomAddNewPage();
        }

        private void Edit_Room_Button_Click(object sender, RoutedEventArgs e)
        {

            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new RoomUpdatePage(selected, this, mainManagerWindow);
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void Delete_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room r = (Room)lvDataBinding.SelectedItem;
                RoomService roomService = new RoomService();
                roomService.DeleteRoom(r.RoomNumber);
                Rooms.Remove(r);
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void View_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItem;
                mainManagerWindow.MainManagerView.Content = new RoomViewPage(selected,this);
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void Renovations_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new RenovationsPage(mainManagerWindow, selected);
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }
    }
}
