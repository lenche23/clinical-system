﻿using Model;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for NewAnnouncement.xaml
    /// </summary>
    public partial class NewAnnouncement : Window
    {
        public NewAnnouncement()
        {
            InitializeComponent();
            Visibility.Items.Add("Svi");
            Visibility.Items.Add("Zaposleni");
            Visibility.Items.Add("Pacijenti");
            Visibility.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementStorage ast = new AnnouncementStorage();
            int id = ast.Load().Count();
            DateTime po = DateTime.Today;
            DateTime ed = DateTime.Today;
            String con = Content.Text;
            String tit = Title.Text;
            Model.Visibility vis = Model.Visibility.all;
            if(Visibility.SelectedIndex == 1)
            {
                vis = Model.Visibility.staff;
            }
            if (Visibility.SelectedIndex == 2)
            {
                vis = Model.Visibility.patients;
            }
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            AnnouncementStorage s = new AnnouncementStorage();
            s.Save(announcement);
            Announcements.Ans.Add(announcement);

            this.Close();

        }
    }
}