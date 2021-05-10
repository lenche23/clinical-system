using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Bolnica;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    /// 

    public partial class DoctorView : Window
    {
        public Doctor DoctorUser;

        public DoctorView()
        {
            InitializeComponent();
            DoctorStorage ds = new DoctorStorage();
            DoctorUser = ds.GetOne("1708962324890");
            Main.Content = new Calendar(this);
        }

        private void CalendarClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new Calendar(this);
        }

        private void MedicineClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePageView(this);
        }

        private void AnnouncementsClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AnnouncementsView(UserType.doctor, this);
        }
    }
}
