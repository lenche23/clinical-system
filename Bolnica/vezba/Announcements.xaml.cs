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

namespace vezba
{
    public partial class Announcements : Window
    {
        public static ObservableCollection<Announcement> Ans { get; set; }
        public Announcements()
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementStorage s = new AnnouncementStorage();
            List<Announcement> temp = s.GetAll();
            Ans = new ObservableCollection<Announcement>(temp);
        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewAnnouncement();
            w.Show();
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                var w = new ViewAnnouncement(a);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                var w = new EditAnnouncement(a);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                AnnouncementStorage s = new AnnouncementStorage();
                s.Delete(a.Id);
                Ans.Remove(a);

            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}