﻿using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomAddEquipmentPage : Page
    {
        private Room selected;
        private MainManagerWindow mainManagerWindow;
        public RoomAddEquipmentPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            this.mainManagerWindow = mainManagerWindow;
            Title.Content = Title.Content + " " + selected.RoomNumber;
            EquipmentService equipmentService = new EquipmentService();
            List<Equipment> equipmentList = equipmentService.GetAllEquipment();
            comboEquipmentName.ItemsSource = equipmentList;
        }
        private void PotvrdiDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            Equipment comboEquipment = (Equipment)comboEquipmentName.SelectedItem;
            var Quantity = int.Parse(Količina.Text);
            var endTime = new DateTime(2999, 1, 1, 0, 0, 0);
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            var id = roomInventoryService.GenerateNextRoomInventoryId();
            RoomInventory newRoomInventory = new RoomInventory(DateTime.Now, endTime, Quantity, id, comboEquipment, this.selected);
            roomInventoryService.SaveRoomInventory(newRoomInventory);
            RoomUpdatePage.RoomInventoryList.Add(newRoomInventory);
            NavigationService.GoBack();
        }
        private void OdustaniDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
