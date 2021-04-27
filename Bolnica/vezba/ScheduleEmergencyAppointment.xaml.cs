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

namespace vezba
{
    /// <summary>
    /// Interaction logic for ScheduleEmergencyAppointment.xaml
    /// </summary>
    public partial class ScheduleEmergencyAppointment : Window
    {
        private Appointment EmergencyAppointment { get; set; }
        public ScheduleEmergencyAppointment(Appointment emergencyAppointment)
        {
            InitializeComponent();
            Option.Text = "Najblizi slobodan termin je " + emergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + emergencyAppointment.DoctorName + ".\n\n";
            Option.Text += "Mozete tada zakazati hitan termin, ili otkazati neki od ranije zakazanih termina.";
            EmergencyAppointment = emergencyAppointment;

        }

        private void Show_Appointments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Schedule_Appointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentStorage aps = new AppointmentStorage();
            Boolean b = aps.Save(EmergencyAppointment);
            if (b)
            {
                SecretaryAppointments.Appointments.Add(EmergencyAppointment);
                MessageBox.Show("Zakazan je hitan termin za " + EmergencyAppointment.StartTime.ToString("dd.MM.yyyy. HH:mm") + " kod lekara " + EmergencyAppointment.DoctorName);
            }
        }
    }
}
