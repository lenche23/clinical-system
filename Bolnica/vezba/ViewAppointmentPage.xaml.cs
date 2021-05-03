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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewAppointmentPage.xaml
    /// </summary>
    public partial class ViewAppointmentPage : Page
    {

        private Appointment Selected { get; set; }


        private DoctorView dw;

        public ViewAppointmentPage(Appointment selected, DoctorView dw)
        {
            InitializeComponent();
            this.DataContext = selected;
            Selected = selected;
            this.dw = dw;
        }

        private void ZdravstveniKartonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new MedicalRecordPage(Selected.Patient, dw);
        }

        private void NovaAnamnezaClick(object sender, RoutedEventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, Selected.StartTime) >= 0)
            {
                dw.Main.Content = new CreateAnamnesisPage(Selected, dw);
            }
            else
            {
                MessageBox.Show("Nije moguće kreirati izveštaj pre termina pregleda!");
            }
        }

        private void NovReceptClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new CreatePrescriptionPage(Selected.Patient, dw);
        }

        private void IzdavanjeUputaClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new CreateReferralLetterPage(Selected.Patient, dw);
        }

        private void PovratakClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
