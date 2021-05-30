﻿using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using Service;

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
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            RoomService roomService = new RoomService();
            Room roomEntry = roomService.GetOneRoom(roomNumber);

            if (Validate(roomEntry) == false)
                return;

            roomInventory.Quantity -= itemQuantity;
            roomInventoryService.UpdateRoomInventory(this.roomInventory);
            windowUpdateRoom.RoomInventoryBinding.Items.Refresh();

            if (!roomInventoryService.AddQuantityToDesiredRoom(roomNumber, roomInventory, itemQuantity))
            {
                roomInventoryService.SaveNewRoomInventory(DateTime.Now, infiniteTime, itemQuantity, roomEntry, roomInventory.equipment);
            }
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

