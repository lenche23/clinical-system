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
    public partial class ViewMyAnnouncements : Window
    {
        public static ObservableCollection<Announcement> Ans { get; set; }
        public ViewMyAnnouncements(UserType ut)
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementStorage s = new AnnouncementStorage();
            List<Announcement> announcements = s.GetByUser(ut);
            Ans = new ObservableCollection<Announcement>(announcements);
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
    }
}