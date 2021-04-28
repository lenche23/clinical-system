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
using Model;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for AppointmentView.xaml
    /// </summary>
    public partial class AppointmentView : Window
    {

        public Appointment appointment;

        public AppointmentView(Appointment selected)
        {
            InitializeComponent();
            this.DataContext = selected;
            appointment = selected;
        }

        private void ZdravstveniKartonClick(object sender, RoutedEventArgs e)
        {
            var s = new MedicalRecordView(appointment.Patient);
            s.Show();
        }

        private void NovaAnamnezaClick(object sender, RoutedEventArgs e)
        {
            if(DateTime.Compare(DateTime.Now, appointment.StartTime) >= 0) 
            {
                var s = new CreateAnamnesisView(appointment);
                s.Show();
            }
            else
            {
                MessageBox.Show("Nije moguće kreirati izveštaj pre termina pregleda!");
            }
        }

        private void NovReceptClick(object sender, RoutedEventArgs e)
        {
            var s = new CreatePrescriptionView(appointment.Patient);
            s.Show();
        }

        private void IzdavanjeUputaClick(object sender, RoutedEventArgs e)
        {
            var s = new CreateReferralLetterView(appointment.Patient);
            s.Show();
        }
    }
}
