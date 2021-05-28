﻿using Bolnica;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeEquipmentPage : Page
    {
        private RoomUpdatePage windowUpdateRoom;
        private Room room;
        private RoomInventory roomInventory;
        private int maximumQuantity;
        private int itemQuantity;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private MainManagerWindow mainManagerWindow;
        private RoomInventoryFileRepository _roomInventoryFileRepository;

        public RoomExchangeEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory roomInventory, RoomUpdatePage windowUpdateRoom, Room room)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            this.windowUpdateRoom = windowUpdateRoom;
            this.roomInventory = roomInventory;
            this.room = room;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int roomNumber = int.Parse(RoomNumberTextBox.Text);
            itemQuantity = int.Parse(ItemQuantity.Text);
            maximumQuantity = roomInventory.Quantity;

            RoomFileRepository roomFileRepository = new RoomFileRepository();
            Room roomEntry = roomFileRepository.GetOne(roomNumber);

            if (Validate(roomEntry) == false)
                return;

            _roomInventoryFileRepository = new RoomInventoryFileRepository();

            RemoveQuantityFromCurrentRoom();

            if (!AddQuantityToDesiredRoom(roomNumber))
            {
                CreateRoomInventory(roomEntry);
            }
            //this.Close();
            NavigationService.GoBack();
        }

        private void CreateRoomInventory(Room roomEntry)
        {
            var id = _roomInventoryFileRepository.GenerateNextId();
            RoomInventory ri = new RoomInventory(DateTime.Now, infiniteTime, itemQuantity, id, roomInventory.equipment, roomEntry);
            _roomInventoryFileRepository.Save(ri);
        }

        private void RemoveQuantityFromCurrentRoom()
        {
            roomInventory.Quantity -= itemQuantity;
            _roomInventoryFileRepository.Update(this.roomInventory);
            windowUpdateRoom.RoomInventoryBinding.Items.Refresh();
        }

        private bool AddQuantityToDesiredRoom(int roomNumber)
        {
            var itemFound = false;
            foreach (RoomInventory inventory in _roomInventoryFileRepository.GetAll())
            {
                if (inventory.room.RoomNumber == roomNumber && inventory.equipment.Id == roomInventory.equipment.Id)
                {
                    inventory.Quantity += itemQuantity;
                    _roomInventoryFileRepository.Update(inventory);
                    itemFound = true;
                }
            }
            return itemFound;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

