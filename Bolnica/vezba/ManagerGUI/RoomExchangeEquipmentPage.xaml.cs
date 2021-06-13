using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Service;
using vezba.Service;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeEquipmentPage : Page, INotifyPropertyChanged
    {
        private RoomUpdatePage windowUpdateRoom;
        private Room room;
        private RoomInventory roomInventory;
        private int maximumQuantity;
        private int itemQuantity;
        private int desiredRoomItemQuantity;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private MainManagerWindow mainManagerWindow;
        public List<Room> roomList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private int kolicinaRobe;
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

        public RoomExchangeEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory roomInventory, RoomUpdatePage windowUpdateRoom, Room room)
        {
            InitializeComponent();
            DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            this.windowUpdateRoom = windowUpdateRoom;
            this.roomInventory = roomInventory;
            this.room = room;

            RoomService roomService = new RoomService();
            roomList = roomService.GetAllRooms();
            List<Room> temporaryList = new List<Room>();

              foreach (Room r in roomList)
            {
                if (DateTime.Compare(room.StartDateTime, DateTime.Now) <= 0 && DateTime.Compare(room.EndDateTime, DateTime.Now) >= 0)
                {
                    temporaryList.Add(r);
                }
            }

            if (RoomToMerge.SelectedIndex == -1 || ItemQuantity.Text == "")
              {
                  OkButton.IsEnabled = false;
              }

              else OkButton.IsEnabled = true;

            RoomToMerge.ItemsSource = temporaryList;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            ReadInformation();
            Room roomEntry = (Room)RoomToMerge.SelectedItem;
            if (!Validate(roomEntry)) return;

            RoomInventoryService roomInventoryService = new RoomInventoryService();

            roomInventory.Quantity -= itemQuantity;
            roomInventoryService.UpdateRoomInventory(this.roomInventory);

            DinamicEquipmentStrategy strategy = new DinamicEquipmentStrategy();

            desiredRoomItemQuantity = roomInventoryService.ChangeEquipmentQuantity(strategy, roomInventory, roomEntry.RoomNumber, itemQuantity, DateTime.Now);

            if (desiredRoomItemQuantity != -1)
            {
                var ri = new RoomInventory(DateTime.Now, infiniteTime, desiredRoomItemQuantity, 0, roomInventory.equipment, roomEntry);
                roomInventoryService.SaveRoomInventory(ri);
            }

            windowUpdateRoom.RoomInventoryBinding.Items.Refresh();
            NavigationService.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ReadInformation()
        {
            itemQuantity = int.Parse(ItemQuantity.Text);
            maximumQuantity = roomInventory.Quantity;
        }

        public Boolean Validate(Room roomEntry)
        {
            if (roomEntry == null)
            {
                //MessageBox.Show("Soba ne postoji");
                return false;
            }

            if (roomEntry.RoomNumber == this.room.RoomNumber)
            {
                //MessageBox.Show("Soba ne može biti trenutno selektovana soba");
                return false;
            }

            if (maximumQuantity < itemQuantity)
            {
                //MessageBox.Show("ItemQuantity robe prekoračava maksimalnu postojeću u sobi");
                return false;
            }

            ItemQuantity.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(ItemQuantity))
            {
                return false;
            }
            return true;
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

        private void ItemQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RoomToMerge.SelectedIndex == -1 || ItemQuantity.Text == "")
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

        private void RoomToMerge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomToMerge.SelectedIndex == -1 || ItemQuantity.Text =="")
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

    }
}

