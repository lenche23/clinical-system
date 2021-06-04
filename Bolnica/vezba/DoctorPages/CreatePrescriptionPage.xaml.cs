using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreatePrescriptionPage.xaml
    /// </summary>
    public partial class CreatePrescriptionPage : Page, INotifyPropertyChanged
    {
        private readonly Patient _patient;
        private readonly DoctorView _doctorView;
        public List<Medicine> ValidMedicine { get; set; }

        private readonly MedicalRecordPage _medicalRecordPage;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _duration;
        public String Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private String _number;
        public String Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public CreatePrescriptionPage(Patient patient, DoctorView doctorView, MedicalRecordPage medicalRecordPage)
        {
            InitializeComponent();
            _patient = patient;
            _doctorView = doctorView;
            DataContext = this;
            var medicineService = new MedicineService();
            ValidMedicine = medicineService.GenerateValidMedicineForPatient(_patient.MedicalRecord);
            this._medicalRecordPage = medicalRecordPage;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if(!ValidateEntries())
                return;
            var newPrescription = NewPrescription();

            var patientService = new PatientService();
            patientService.AddPrescriptionToPatient(_patient, newPrescription);
            _medicalRecordPage.PrescriptionListView.Items.Refresh();

            _doctorView.Main.GoBack();
        }
        private Prescription NewPrescription()
        {
            var startDate = (DateTime) DpStartDate.SelectedDate;
            var durationInDays = int.Parse(Duration);
            var referencePeriod = (CmbPeriod.SelectedIndex == 0) ? Period.daily : Period.weekly;
            var number = int.Parse(Number);
            var selectedMedicine = (Medicine) CmbMedicine.SelectedItem;
            return new Prescription(startDate, durationInDays, referencePeriod, number, 0, true, selectedMedicine);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private Boolean ValidateEntries()
        {
            if (DpStartDate.SelectedDate == null)
                return false;
            int r;
            if (!int.TryParse(TbDuration.Text, out r) || int.Parse(TbDuration.Text) == 0)
                return false;
            if (!int.TryParse(TbNumber.Text, out r) || int.Parse(TbNumber.Text) == 0)
                return false;
            return true;
        }
    }
}
