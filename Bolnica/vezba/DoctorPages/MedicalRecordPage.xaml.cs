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
        private readonly DoctorView _doctorView;

        public MedicalRecordPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = patient;
            if (patient.MedicalRecord != null)
            {
                _doctorView = doctorView;
            }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}

