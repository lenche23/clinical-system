using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateAnamnesisPage.xaml
    /// </summary>
    public partial class CreateAnamnesisPage : Page
    {
        private readonly Patient _patient;

        private readonly DoctorView _doctorView;

        public CreateAnamnesisPage(Appointment appointment, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = appointment;
            _patient = appointment.Patient;
            _doctorView = doctorView;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var newAnamnesis = NewAnamnesis();

            var patientService = new PatientService();
            patientService.AddAnamnesisToPatient(_patient, newAnamnesis);

            _doctorView.Main.GoBack();
        }

        private Anamnesis NewAnamnesis()
        {
            var comment = TbComment.Text;
            var patient = PatientNameSurname.Text;
            var doctor = DoctorNameSurname.Text;
            var time = DateTime.Now;
            var newAnamnesis = new Anamnesis(time, comment, patient, doctor);
            return newAnamnesis;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
