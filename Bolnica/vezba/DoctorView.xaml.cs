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
        public DoctorView()
        {
            InitializeComponent();
            Main.Content = new CalendarView(this);
        }

        private void CalendarClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new CalendarView(this);
        }

        private void MedicineClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePageView();
        }

        private void AnnouncementsClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AnnouncementsView(UserType.doctor);
        }
    }
}
