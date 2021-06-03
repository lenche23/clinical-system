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
        private readonly Patient _patient;

        public MedicalRecordPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            _patient = patient;
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

        private void ExtendButtonClick(object sender, RoutedEventArgs e)
        {
            if (HospitalTreatmentListView.SelectedItems.Count > 0)
            {
                var hospitalTreatment = (HospitalTreatment) HospitalTreatmentListView.SelectedItem;
                _doctorView.Main.Content = new ExtendHospitalTreatment(_patient, hospitalTreatment, _doctorView, this);
            }
        }

        private void NewPrescriptionClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new CreatePrescriptionPage(_patient, _doctorView, this);
        }

        private void NewReferralLetterClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new CreateReferralLetterPage(_patient, _doctorView, this);
        }

        private void NewHospitalTreatmentClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new CreateHospitalTreatment(_patient, _doctorView, this);
        }

        private void ViewPrescriptionClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var prescription = (sender as Grid).DataContext as Prescription;
            _doctorView.Main.Content = new ViewPrescription(prescription, _doctorView);
        }

        private void ViewAnamnesisClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var anamnesis = (sender as Grid).DataContext as Anamnesis;
            _doctorView.Main.Content = new ViewAnamnesis(anamnesis, _doctorView);
        }
    }
}

