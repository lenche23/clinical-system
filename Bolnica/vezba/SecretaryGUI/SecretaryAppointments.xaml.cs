using Model;
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
            InitializeComponent();
            this.DataContext = this;
            AppointmentFileRepository s = new AppointmentFileRepository();
            List<Appointment> allAppointments = s.GetAll();
            Appointments = new ObservableCollection<Appointment>(allAppointments);
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewAppointment s = new SecretaryNewAppointment();
            s.Show();
        }

        private void NewEmergencyAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewEmergencyAppointment s = new SecretaryNewEmergencyAppointment();
            //var s = new EmergencyAppointment();
            s.Show();
        }

        private void ViewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                //var s = new ViewAppointment(selectedAppointment);
                SecretaryViewAppointment s = new SecretaryViewAppointment(selectedAppointment);
                s.Show();
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        private void EditAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                //var s = new EditAppointment(selectedAppointment);
                SecretaryEditAppointment s = new SecretaryEditAppointment(selectedAppointment);
                s.Show();
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedCells.Count > 0)
            {
                Appointment selectedAppointment = (Appointment)appointmentsTable.SelectedItem;
                AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
                appointmentFileRepository.Delete(selectedAppointment.AppointentId);
                Appointments.Remove(selectedAppointment);
            }
            else
                MessageBox.Show("Niste selektovali termin!");
        }
    }
}
