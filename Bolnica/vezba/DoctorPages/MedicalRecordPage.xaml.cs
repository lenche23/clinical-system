using System.Windows;
using System.Windows.Controls;
using Model;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private DoctorView doctorView;

        public MedicalRecordPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = patient;
            if (patient.MedicalRecord != null)
            {
                this.doctorView = doctorView;
            }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }
    }
}

