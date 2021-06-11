using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeStaticEquipmentPage : Page, INotifyPropertyChanged
    {
        private RoomInventory roomInventory;
        private Room room;
        private DateTime pickedDate;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private int inputItemQuantity;
        private int currentRoomItemQuantity;
        private int desiredRoomItemQuantity;
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


        public RoomExchangeStaticEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory roomInventory, Room room)
        {
            InitializeComponent();
            DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            this.roomInventory = roomInventory;
            this.room = room;
            Date.SelectedDate = DateTime.Now;

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

            OkButton.IsEnabled = false;
            RoomToMerge.ItemsSource = temporaryList;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            Room roomEntry = (Room)RoomToMerge.SelectedItem;
            inputItemQuantity = int.Parse(Količina.Text);
            currentRoomItemQuantity = roomInventory.Quantity;
            pickedDate = Date.SelectedDate.Value.Date;
            if (pickedDate == null)
            {
                pickedDate = DateTime.Now;
            }
            //pickedDate = DateTime.Now.AddSeconds(5);
            RoomInventoryService roomInventoryService = new RoomInventoryService();

            if (!Validate(roomEntry)) return;

            roomInventory.EndTime = pickedDate;
            roomInventoryService.UpdateRoomInventory(this.roomInventory);

            currentRoomItemQuantity = roomInventory.Quantity - inputItemQuantity;
            desiredRoomItemQuantity = roomInventoryService.NewDesiredRoomItemQuantity(roomInventory, roomEntry.RoomNumber, inputItemQuantity, pickedDate);
            var ri1 = new RoomInventory(pickedDate, infiniteTime, currentRoomItemQuantity, 0, roomInventory.equipment, room);
            roomInventoryService.SaveRoomInventory(ri1);
            var ri2 = new RoomInventory(pickedDate, infiniteTime, desiredRoomItemQuantity, 0, roomInventory.equipment, roomEntry);
            roomInventoryService.SaveRoomInventory(ri2);

            NavigationService.GoBack();
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

            if (currentRoomItemQuantity < inputItemQuantity)
            {
                //MessageBox.Show("Uneta količina robe prekoračava maksimalnu postojeću u sobi");
                return false;
            }

            if (DateTime.Compare(pickedDate, DateTime.Now) < 0)
            {
                //MessageBox.Show("Izabrani datum je već prošao!");
                return false;
            }

            Količina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(Količina))
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

        private void RoomToMerge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomToMerge.SelectedIndex == -1 || Količina.Text == "")
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

        private void Količina_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RoomToMerge.SelectedIndex == -1 || Količina.Text == "")
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }
    }
}
