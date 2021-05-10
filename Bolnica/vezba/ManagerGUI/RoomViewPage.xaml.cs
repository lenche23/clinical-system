﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using vezba;

namespace vezba.ManagerGUI
{
    public partial class RoomViewPage : Page
{
        private Room selected;
        private ManagerView mv;
        public static ObservableCollection<RoomInventory> RoomInventoryList { get; set; }
        private RoomInventoryStorage roomInventoryStorage;
        public RoomViewPage(Room selected, ManagerView mv)
        {
            InitializeComponent();
            this.selected = selected;
            this.mv = mv;


            BrProstorijeLabel.Content = BrProstorijeLabel.Content + " " + selected.RoomNumber;

            if (selected.RoomFloor == Floor.first)
            {
                SpratLabel.Content = SpratLabel.Content + " Prvi";
            }
            else if (selected.RoomFloor == Floor.second)
            {
                SpratLabel.Content = SpratLabel.Content + " Drugi";
            }
            else if (selected.RoomFloor == Floor.third)
            {
                SpratLabel.Content = SpratLabel.Content + " Treci";
            }


            if (selected.RoomType == RoomType.examinationRoom)
            {
                TipLabel.Content = TipLabel.Content + " Prostorija za pregled";
            }
            else if (selected.RoomType == RoomType.operatingRoom)
            {
                TipLabel.Content = TipLabel.Content + " Operaciona sala";
            }
            else if (selected.RoomType == RoomType.recoveryRoom)
            {
                TipLabel.Content = TipLabel.Content + " Prostorija za odmor";
            }

            roomInventoryStorage = new RoomInventoryStorage();

            List<RoomInventory> roomInventoryList = new List<RoomInventory>();
            foreach (RoomInventory roomInventory in roomInventoryStorage.GetAll())
            {
                if (roomInventory.room.RoomNumber == selected.RoomNumber)
                {
                    if (DateTime.Compare(roomInventory.StartTime, DateTime.Now) <= 0 &&
                        DateTime.Compare(roomInventory.EndTime, DateTime.Now) >= 0)
                    {
                        roomInventoryList.Add(roomInventory);
                    }
                }
            }

            RoomInventoryList = new ObservableCollection<RoomInventory>(roomInventoryList);
            RoomInventoryBinding.ItemsSource = RoomInventoryList;
        }
    }
}
