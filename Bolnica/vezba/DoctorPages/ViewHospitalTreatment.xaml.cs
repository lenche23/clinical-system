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
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for ViewHospitalTreatment.xaml
    /// </summary>
    public partial class ViewHospitalTreatment : Page
    {
        private readonly DoctorView _doctorView;
        private readonly Patient _patient;
        private readonly HospitalTreatment _hospitalTreatment;
        private readonly MedicalRecordPage _medicalRecordPage;

        public ViewHospitalTreatment(HospitalTreatment hospitalTreatment, DoctorView doctorView, Patient patient, MedicalRecordPage medicalRecordPage)
        {
            InitializeComponent();
            _doctorView = doctorView;
            _patient = patient;
            _hospitalTreatment = hospitalTreatment;
            _medicalRecordPage = medicalRecordPage;
            DataContext = _hospitalTreatment;
        }
        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var patientService = new PatientService();
            patientService.RemoveHospitalTreatmentFromPatient(_patient, _hospitalTreatment);
            _medicalRecordPage.HospitalTreatmentListView.Items.Refresh();
            _doctorView.Main.GoBack();
        }
    }
}
