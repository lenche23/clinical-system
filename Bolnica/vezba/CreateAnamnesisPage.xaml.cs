using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Service;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for CreateAnamnesisPage.xaml
    /// </summary>
    public partial class CreateAnamnesisPage : Page
    {
        private readonly Patient _patient;

        private readonly DoctorView _doctorView;

        private Anamnesis _newAnamnesis;

        public CreateAnamnesisPage(Appointment appointment, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = appointment;
            _patient = appointment.Patient;
            _doctorView = doctorView;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            _newAnamnesis = NewAnamnesis();

            PatientService patientService = new PatientService();
            patientService.AddAnamnesisToPatient(_patient, _newAnamnesis);

            AppointmentService appointmentService = new AppointmentService();
            appointmentService.AddAnamnesisToAppointments(_patient, _newAnamnesis);

            UpdateAppointmentsView();
            _doctorView.Main.GoBack();
        }

        private void UpdateAppointmentsView()
        {
            foreach (Appointment appointment in Calendar.appointments)
            {
                if (appointment.Patient.Jmbg.Equals(_patient.Jmbg))
                    appointment.Patient.MedicalRecord.AddAnamnesis(_newAnamnesis);
            }
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
