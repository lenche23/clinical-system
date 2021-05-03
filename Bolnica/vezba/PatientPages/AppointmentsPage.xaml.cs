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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba.PatientPages
{
    public partial class AppointmentsPage : Page
    {
        public AppointmentsPage()
        {
            InitializeComponent();
        }

        private void ButtonOrderAppointment_Click(object sender, RoutedEventArgs e)
        {
            var s = new OrderAppointmentPopup();
            s.Show();
        }

        private void ButtonChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            var izmeniPregled = new ChangeAppointmentView();
            izmeniPregled.Show();
        }

        private void ButtonCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            var otkaziPregled = new CancelAppointmentView();
            otkaziPregled.Show();
        }

        private void ButtonHistoryAppointment_Click(object sender, RoutedEventArgs e)
        {
            var istorijaPregleda = new AppointmentHistory();
            istorijaPregleda.Show();
        }
    }
}
