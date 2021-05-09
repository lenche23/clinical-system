using Bolnica;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeEquipmentPage : Page
    {
        private WindowUpdateRoom windowUpdateRoom;
        private Room room;
        private RoomInventory roomInventory;
        private int maximumQuantity;
        private int itemQuantity;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private RoomInventoryStorage roomInventoryStorage;

        public RoomExchangeEquipmentPage(RoomInventory roomInventory, WindowUpdateRoom windowUpdateRoom, Room room)
        {
            InitializeComponent();
            this.windowUpdateRoom = windowUpdateRoom;
            this.roomInventory = roomInventory;
            this.room = room;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int roomNumber = int.Parse(RoomNumberTextBox.Text);
            itemQuantity = int.Parse(ItemQuantity.Text);
            maximumQuantity = roomInventory.Quantity;

            RoomStorage roomStorage = new RoomStorage();
            Room roomEntry = roomStorage.GetOne(roomNumber);

            if (Validate(roomEntry) == false)
                return;

            roomInventoryStorage = new RoomInventoryStorage();

            RemoveQuantityFromCurrentRoom();

            if (!AddQuantityToDesiredRoom(roomNumber))
            {
                CreateRoomInventory(roomEntry);
            }
            //this.Close();
        }

        private void CreateRoomInventory(Room roomEntry)
        {
            var id = roomInventoryStorage.GenerateNextId();
            RoomInventory ri = new RoomInventory(DateTime.Now, infiniteTime, itemQuantity, id, roomInventory.equipment, roomEntry);
            roomInventoryStorage.Save(ri);
        }

        private void RemoveQuantityFromCurrentRoom()
        {
            roomInventory.Quantity -= itemQuantity;
            roomInventoryStorage.Update(this.roomInventory);
            windowUpdateRoom.RoomInventoryBinding.Items.Refresh();
        }

        private bool AddQuantityToDesiredRoom(int roomNumber)
        {
            var itemFound = false;
            foreach (RoomInventory inventory in roomInventoryStorage.GetAll())
            {
                if (inventory.room.RoomNumber == roomNumber && inventory.equipment.Id == roomInventory.equipment.Id)
                {
                    inventory.Quantity += itemQuantity;
                    roomInventoryStorage.Update(inventory);
                    itemFound = true;
                }
            }
            return itemFound;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        public Boolean Validate(Room roomEntry)
        {
            if (roomEntry == null)
            {
                MessageBox.Show("Soba ne postoji");
                return false;
            }

            if (roomEntry.RoomNumber == this.room.RoomNumber)
            {
                MessageBox.Show("Soba ne može biti trenutno selektovana soba");
                return false;
            }

            if (maximumQuantity < itemQuantity)
            {
                MessageBox.Show("ItemQuantity robe prekoračava maksimalnu postojeću u sobi");
                return false;
            }
            return true;
        }
    }
}

