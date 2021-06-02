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
    /// Interaction logic for ExtendHospitalTreatment.xaml
    /// </summary>
    public partial class ExtendHospitalTreatment : Page
    {
        public HospitalTreatment HospitalTreatment { get; set; }
        private readonly DoctorView _doctorView;
        private readonly Patient _patient;
        private readonly MedicalRecordPage _medicalRecordPage;
        public ExtendHospitalTreatment(Patient patient, HospitalTreatment hospitalTreatment, DoctorView doctorView, MedicalRecordPage medicalRecordPage)
        {
            InitializeComponent();
            HospitalTreatment = hospitalTreatment;
            _doctorView = doctorView;
            _patient = patient;
            _medicalRecordPage = medicalRecordPage;
            DataContext = HospitalTreatment;
            TbStartDate.Text = HospitalTreatment.StartDate.ToString("d");
            TbDuration.Text = hospitalTreatment.DurationInDays.ToString();
            TbRoom.Text = HospitalTreatment.Room.RoomNumber.ToString();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            HospitalTreatment.DurationInDays = int.Parse(TbDuration.Text);
            PatientService patientService = new PatientService();
            patientService.EditPatient(_patient);
            _medicalRecordPage.HospitalTreatmentListView.Items.Refresh();
            _doctorView.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
