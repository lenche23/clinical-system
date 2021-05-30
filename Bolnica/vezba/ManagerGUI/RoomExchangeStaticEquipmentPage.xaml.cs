﻿using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeStaticEquipmentPage : Page
    {
        private RoomInventory roomInventory;
        private Room room;
        private DateTime pickedDate;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private int inputItemQuantity;
        private int currentRoomItemQuantity;
        private int desiredRoomItemQuantity;
        private MainManagerWindow mainManagerWindow;

        public RoomExchangeStaticEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory roomInventory, Room room)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            this.roomInventory = roomInventory;
            this.room = room;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int roomNumber = int.Parse(BrojSobe.Text);
            inputItemQuantity = int.Parse(Količina.Text);
            currentRoomItemQuantity = roomInventory.Quantity;
            //pickedDate = Date.SelectedDate.Value.Date;
            pickedDate = DateTime.Now.AddSeconds(5);

            RoomService roomService = new RoomService();
            Room roomEntry = roomService.GetOneRoom(roomNumber);
            RoomInventoryService roomInventoryService = new RoomInventoryService();

            if (Validate(roomEntry) == false)
                return;

            roomInventory.EndTime = pickedDate;
            roomInventoryService.UpdateRoomInventory(this.roomInventory);

            currentRoomItemQuantity = roomInventory.Quantity - inputItemQuantity;
            desiredRoomItemQuantity = roomInventoryService.NewDesiredRoomItemQuantity(roomInventory, roomNumber, inputItemQuantity, pickedDate);
            roomInventoryService.SaveNewRoomInventory(pickedDate, infiniteTime, currentRoomItemQuantity, room, roomInventory.equipment);
            roomInventoryService.SaveNewRoomInventory(pickedDate, infiniteTime, desiredRoomItemQuantity, roomEntry, roomInventory.equipment);

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

            if (currentRoomItemQuantity < inputItemQuantity)
            {
                MessageBox.Show("Uneta količina robe prekoračava maksimalnu postojeću u sobi");
                return false;
            }

            if (DateTime.Compare(pickedDate, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je već prošao!");
                return false;
            }
            return true;
        }
    }
}
