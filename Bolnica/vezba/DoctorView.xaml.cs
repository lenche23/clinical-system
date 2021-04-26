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

        public static ObservableCollection<Appointment> Appointments { get; set; }

        private AppointmentStorage storage;

        public DoctorView()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new AppointmentStorage();
            List<Appointment> appointments = storage.GetAll();
            Appointments = new ObservableCollection<Appointment>(appointments);
            listViewAppointments.ItemsSource = Appointments;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var s = new CreateAppointmentView();
            s.Show();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                var s = new EditAppointmentView(selected, this);
                s.Show();
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    storage.Delete(selected.AppointentId);
                    Appointments.Remove(selected);
                }
            }
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                var s = new AppointmentView(selected);
                s.Show();
            }
        }

        private void AnnouncmentsClick(object sender, RoutedEventArgs e)
        {
            var s = new ViewMyAnnouncements(UserType.doctor);
            s.Show();
        }
    }
}
