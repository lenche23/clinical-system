using Model;
using Service;
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

namespace vezba.SecretaryGUI
{
    public partial class SecretaryAppointments : Page
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public SecretaryAppointments()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentService s = new AppointmentService();
            Appointments = new ObservableCollection<Appointment>(s.GetAllAppointments());
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewAppointment w = new SecretaryNewAppointment();
            w.Show();
        }

        private void NewEmergencyAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewEmergencyAppointment w = new SecretaryNewEmergencyAppointment();
            w.Show();
        }

        private void ViewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                SecretaryViewAppointment w = new SecretaryViewAppointment(selectedAppointment);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        private void EditAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                SecretaryEditAppointment w = new SecretaryEditAppointment(selectedAppointment);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                AppointmentService appointmentService = new AppointmentService();
                appointmentService.DeleteAppointment(selectedAppointment.AppointentId);
                Appointments.Remove(selectedAppointment);
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }
    }
}
