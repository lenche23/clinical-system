using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomUpdatePage : Page
    {
        private Room selected;
        private RoomsPage rp;
        public static ObservableCollection<RoomInventory> RoomInventoryList { get; set; }
        private MainManagerWindow mainManagerWindow;

        public RoomUpdatePage(Room selected, RoomsPage rp, MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.selected = selected;
            this.rp = rp;
            this.mainManagerWindow = mainManagerWindow;
            this.DataContext = selected;

            RoomInventoryService roomInventoryService = new RoomInventoryService();

            var roomInventoryList = roomInventoryService.GetAllRoomInventories(selected);

            RoomTextBlock.Text = RoomTextBlock.Text + " " + selected.RoomNumber;

            List<string> floors = new List<string> { "Prvi", "Drugi", "Treći" };
            comboFloor.ItemsSource = floors;

            List<string> types = new List<string> { "Soba za preglede", "Soba za odmor", "Operaciona sala", "Magacin" };
            comboRoomType.ItemsSource = types;

            if (selected.RoomFloor == Floor.first)
            {
                comboFloor.SelectedIndex = 0;
            }
            else if (selected.RoomFloor == Floor.second)
            {
                comboFloor.SelectedIndex = 1;
            }
            else if (selected.RoomFloor == Floor.third)
            {
                comboFloor.SelectedIndex = 2;
            }

            if (selected.RoomType == RoomType.examinationRoom)
            {
                comboRoomType.SelectedIndex = 0;
            }
            else if (selected.RoomType == RoomType.recoveryRoom)
            {
                comboRoomType.SelectedIndex = 1;
            }
            else if (selected.RoomType == RoomType.operatingRoom)
            {
                comboRoomType.SelectedIndex = 2;
            }
            else if (selected.RoomType == RoomType.storageRoom)
            {
                comboRoomType.SelectedIndex = 3;
            }

            RoomInventoryList = new ObservableCollection<RoomInventory>(roomInventoryList);
            RoomInventoryBinding.ItemsSource = RoomInventoryList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(RoomInventoryBinding.ItemsSource);
            RoomInventoryBinding.Items.Refresh();
        }

        private void Potvrda_Button_Click(object sender, RoutedEventArgs e)
        {
            //var roomNumber = int.Parse(BrojSobe.Text);
            //selected.RoomNumber = roomNumber;

            if (comboFloor.SelectedIndex == 0) selected.RoomFloor = Floor.first;
            else if (comboFloor.SelectedIndex == 1) selected.RoomFloor = Floor.second;
            else if (comboFloor.SelectedIndex == 2) selected.RoomFloor = Floor.third;

            if (comboRoomType.SelectedIndex == 0) selected.RoomType = RoomType.examinationRoom;
            else if (comboRoomType.SelectedIndex == 1) selected.RoomType = RoomType.recoveryRoom;
            else if (comboRoomType.SelectedIndex == 2) selected.RoomType = RoomType.operatingRoom;
            else if (comboRoomType.SelectedIndex == 3) selected.RoomType = RoomType.storageRoom;

            RoomService roomService = new RoomService();
            roomService.UpdateRoom(selected);
            rp.lvDataBinding.Items.Refresh();
            NavigationService.GoBack();
        }

        private void Odustanak_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Dodaj_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomAddEquipmentPage(mainManagerWindow, selected);
        }

        private void Izmeni_Količinu_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomInventoryBinding.SelectedIndex > -1)
            {
                RoomInventory selectedRoomInventory = (RoomInventory)RoomInventoryBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new RoomChangeEquipmentPage(mainManagerWindow, selectedRoomInventory, this);
            }
            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }
        private void Razmena_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomInventoryBinding.SelectedIndex > -1)
            {
                RoomInventory roomInventorySelected = (RoomInventory)RoomInventoryBinding.SelectedItems[0];
                if (roomInventorySelected.equipment.Type == EquipmentType.dinamical)
                {
                    mainManagerWindow.MainManagerView.Content = new RoomExchangeEquipmentPage(mainManagerWindow, roomInventorySelected, this, selected);
                }
                else if (roomInventorySelected.equipment.Type == EquipmentType.statical)
                {
                    mainManagerWindow.MainManagerView.Content = new RoomExchangeStaticEquipmentPage(mainManagerWindow, roomInventorySelected, selected);
                }
            }
            else
            {
                MessageBox.Show("Ni jedan proizvod nije selektovan!");
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            RoomService roomService = new RoomService();
            roomService.DeleteRoom(selected.RoomNumber);
            RoomsPage.Rooms.Remove(selected);
            mainManagerWindow.MainManagerView.NavigationService.GoBack();
        }

        private void Izbriši_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomInventoryBinding.SelectedIndex > -1)
            {
                RoomInventory ri = (RoomInventory)RoomInventoryBinding.SelectedItem;
                RoomInventoryService roomInventoryService = new RoomInventoryService();
                roomInventoryService.DeleteRoomInventory(ri.Id);
                RoomInventoryList.Remove(ri);
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }
    }
}

