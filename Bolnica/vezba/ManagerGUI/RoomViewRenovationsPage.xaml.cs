﻿using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomViewRenovationsPage : Page
    {
        private Room selected;
        public static ObservableCollection<Renovation> RenovationList { get; set; }
        public RoomViewRenovationsPage(Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            List<Renovation> renovationList = selected.renovation;
            RenovationList = new ObservableCollection<Renovation>(renovationList);
            RenovationsBinding.ItemsSource = RenovationList;
        }

        private void Renovation_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new WindowRenovation(selected);
            s.Show();
        }

        private void Discard_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RenovationsBinding.SelectedIndex > -1)
            {
                Renovation renovation = (Renovation)RenovationsBinding.SelectedItem;
                this.selected.renovation.Remove(renovation);
                RenovationList.Remove(renovation);
                RoomService roomService = new RoomService();
                roomService.UpdateRoom(this.selected);
            }
            else
            {
                MessageBox.Show("Ni jedna rezervacija nije selektovana!");
            }
        }
    }
}

