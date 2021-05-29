using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
using Calendar = vezba.DoctorPages.Calendar;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreatePrescriptionPage.xaml
    /// </summary>
    public partial class CreatePrescriptionPage : Page
    {
        private readonly Patient _patient;
        private readonly DoctorView _doctorView;
        private Prescription _newPrescription;
        public List<Medicine> ValidMedicine { get; set; }

        public CreatePrescriptionPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            _patient = patient;
            _doctorView = doctorView;
            DataContext = this;
            var medicineService = new MedicineService();
            ValidMedicine = medicineService.GenerateValidMedicineForPatient(_patient.MedicalRecord);
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            _newPrescription = NewPrescription();

            var patientService = new PatientService();
            patientService.AddPrescriptionToPatient(_patient, _newPrescription);

            UpdateAppointmentsView();
            _doctorView.Main.GoBack();
        }

        private void UpdateAppointmentsView()
        {
            foreach (var appointment in Calendar.appointments)
            {
                if (appointment.Patient.Jmbg.Equals(_patient.Jmbg))
                    appointment.Patient.MedicalRecord.AddPrescription(_newPrescription);
            }
        }
        private Prescription NewPrescription()
        {
            var startDate = (DateTime) DpStartDate.SelectedDate;
            var durationInDays = int.Parse(TbDuration.Text);
            var referencePeriod = (CmbPeriod.SelectedIndex == 0) ? Period.daily : Period.weekly;
            var number = int.Parse(TbNumber.Text);
            var selectedMedicine = (Medicine) CmbMedicine.SelectedItem;
            return new Prescription(startDate, durationInDays, referencePeriod, number, 0, true, selectedMedicine);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
