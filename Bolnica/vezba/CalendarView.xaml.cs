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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bolnica;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }

        private AppointmentStorage storage;

        private Doctor DoctorUser;

        private DoctorView dw;

        public CalendarView(DoctorView dw)
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new AppointmentStorage();
            List<Appointment> appointments = storage.GetAll();
            Appointments = new ObservableCollection<Appointment>(appointments);
            listViewAppointments.ItemsSource = Appointments;
            DoctorStorage ds = new DoctorStorage();
            DoctorUser = ds.GetOne("1708962324890");
            this.dw = dw;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new CreateAppointmentPage(dw);
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
    }
}

