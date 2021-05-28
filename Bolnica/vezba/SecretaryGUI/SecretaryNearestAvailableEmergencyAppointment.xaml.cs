using Model;
using Service;
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
    public partial class SecretaryNearestAvailableEmergencyAppointment : Window
    {
        private Appointment EmergencyAppointment { get; set; }
        public SecretaryNearestAvailableEmergencyAppointment(Appointment emergencyAppointment)
        {
            InitializeComponent();
            TextBlock.Text = "Najblizi slobodan termin je " + emergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + emergencyAppointment.DoctorName + ".\n\n";
            TextBlock.Text += "Mozete tada zakazati hitan termin, ili otkazati neki od ranije zakazanih termina.";
            EmergencyAppointment = emergencyAppointment;
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            AppointmentService aps = new AppointmentService();
            Boolean b = aps.SaveAppointment(EmergencyAppointment);
            if (b)
            {
                SecretaryAppointments.Appointments.Add(EmergencyAppointment);
                MessageBox.Show("Zakazan je hitan termin za " + EmergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + EmergencyAppointment.DoctorName);

                this.Close();
            }
            else
                MessageBox.Show("Neuspesno zakazivanje");
        }

        private void ShowOverlapingAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryOverlapingAppointments w = new SecretaryOverlapingAppointments(EmergencyAppointment);
            w.Show();
            this.Close();
        }
    }
}
