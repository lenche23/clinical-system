using System;
using System.Collections.Generic;
using System.Windows;
using Bolnica;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        private AppointmentStorage storage;

        public DoctorView()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = AppointmentStorage.Instance;
            List<Appointment> appointments = storage.GetAll();
            listViewAppointments.ItemsSource = appointments;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var s = new CreateAppointmentView(this);
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
                    storage.Delete(selected);
                    listViewAppointments.Items.Refresh();
                }
            }
        }
    }
}
