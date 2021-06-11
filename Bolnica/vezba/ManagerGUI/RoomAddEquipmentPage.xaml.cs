using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomAddEquipmentPage : Page, INotifyPropertyChanged
    {
        private Room selected;
        private MainManagerWindow mainManagerWindow;


        private int kolicinaRobe;

        public event PropertyChangedEventHandler PropertyChanged;

        public int KolicinaRobe
        {
            get
            {
                return kolicinaRobe;
            }
            set
            {
                if (value != kolicinaRobe)
                {
                    kolicinaRobe = value;
                    OnPropertyChanged("KolicinaRobe");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public RoomAddEquipmentPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            DataContext = this;
            this.selected = selected;
            this.mainManagerWindow = mainManagerWindow;
            EquipmentService equipmentService = new EquipmentService();
            List<Equipment> equipmentList = equipmentService.GetAllEquipment();
            comboEquipmentName.ItemsSource = equipmentList;
        }
        private void PotvrdiDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateEntries()) return;
            Equipment comboEquipment = (Equipment)comboEquipmentName.SelectedItem;
            var Quantity = int.Parse(Količina.Text);
            var endTime = new DateTime(2999, 1, 1, 0, 0, 0);
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            RoomInventory newRoomInventory = new RoomInventory(DateTime.Now, endTime, Quantity, 0, comboEquipment, this.selected);
            roomInventoryService.SaveRoomInventory(newRoomInventory);
            RoomUpdatePage.RoomInventoryList.Add(newRoomInventory);
            NavigationService.GoBack();
        }
        private void OdustaniDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

        private void Količina_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Količina.Text == "" || comboEquipmentName.SelectedIndex == -1)
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

        private void comboEquipmentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Količina.Text == "" || comboEquipmentName.SelectedIndex==-1)
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

        private Boolean ValidateEntries()
        {
            Količina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(Količina))
            {
                return false;
            }
            return true;
        }
    }
}
