using Model;
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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryViewAppointment.xaml
    /// </summary>
    public partial class SecretaryViewAppointment : Window
    {
        public SecretaryViewAppointment(Appointment appointment)
        {
            InitializeComponent();
            Patient.Content = appointment.PatientName;
            Doctor.Content = appointment.DoctorName;
            Room.Content = appointment.RoomName;
            Date.Content = appointment.StartTime.ToString("dd.MM.yyyy.");
            Time.Content = appointment.StartTime.ToString("HH:mm");
            Duration.Content = appointment.DurationInMunutes;
            Description.Text = appointment.ApointmentDescription;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
