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
    /// Interaction logic for CreateReferralLetterPage.xaml
    /// </summary>
    public partial class CreateReferralLetterPage : Page
    {
        public List<Doctor> Doctors { get; set; }

        private readonly Patient _patient;

        private readonly DoctorView _doctorView;

        private ReferralLetter _newReferralLetter;

        public CreateReferralLetterPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = patient;

            DoctorService doctorService = new DoctorService();
            Doctors = doctorService.GetAll();

            cmbDoctors.DataContext = this;
            cmbDoctors.SelectedIndex = 0;
            _patient = patient;
            _doctorView = doctorView;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        { 
            _newReferralLetter = NewReferralLetter();

            PatientService patientService = new PatientService();
            patientService.AddReferralLetterToPatient(_patient, _newReferralLetter);

            AppointmentService appointmentService = new AppointmentService();
            appointmentService.AddReferralLetterToAppointments(_patient, _newReferralLetter);

            UpdateAppointmentsView();
            _doctorView.Main.GoBack();
        }

        private void UpdateAppointmentsView()
        {
            foreach (Appointment appointment in Calendar.appointments)
            {
                if (appointment.Patient.Jmbg.Equals(_patient.Jmbg))
                {
                    appointment.Patient.MedicalRecord.AddReferralLetter(_newReferralLetter);
                }
            }
        }

        private ReferralLetter NewReferralLetter()
        {
            var startDate = (DateTime) DpStartDate.SelectedDate;
            var durationPeriodInDays = int.Parse(TbDuration.Text);
            var doctor = (Doctor) cmbDoctors.SelectedItem;
            var newReferralLetter = new ReferralLetter(startDate, durationPeriodInDays, doctor);
            return newReferralLetter;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
