using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace vezba
{
    /// <summary>
    /// Interaction logic for CreatePrescriptionPage.xaml
    /// </summary>
    public partial class CreatePrescriptionPage : Page
    {
        private readonly Patient _patient;
        private readonly MedicineStorage _medicineStorage;
        private readonly DoctorView _doctorView;
        private Prescription _newPrescription;
        public List<Medicine> ValidMedicine { get; set; }

        public CreatePrescriptionPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            this._patient = patient;
            this._doctorView = doctorView;
            _medicineStorage = new MedicineStorage();
            DataContext = this;
            GenerateValidMedicine();
        }

        private void GenerateValidMedicine()
        {
            ValidMedicine = new List<Medicine>();
            foreach (var medicine in _medicineStorage.GetApproved())
            {
                if (!AllergenMatchFound(medicine))
                    ValidMedicine.Add(medicine);
            }
        }

        private bool AllergenMatchFound(Medicine medicine)
        {
            var allergenMatchFound = false;
            foreach (var ingredient in medicine.ingridient)
            {
                foreach (var allergen in _patient.MedicalRecord.Allergen)
                {
                    if (ingredient.Name.Equals(allergen.Name))
                    {
                        allergenMatchFound = true;
                    }
                }
            }

            return allergenMatchFound;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            NewPrescription();
            AddPrescriptionToPatient();
            AddPrescriptionToAppointments();
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

        private void AddPrescriptionToAppointments()
        {
            var appointmentStorage = new AppointmentStorage();
            foreach (var appointment in appointmentStorage.GetAll())
            {
                if (!appointment.Patient.Jmbg.Equals(_patient.Jmbg)) continue;

                appointment.Patient.MedicalRecord.AddPrescription(_newPrescription);
                appointmentStorage.Update(appointment);
            }
        }

        private void AddPrescriptionToPatient()
        {
            _patient.MedicalRecord.AddPrescription(_newPrescription);
            var patientStorage = new PatientStorage();
            patientStorage.Update(_patient);
        }

        private void NewPrescription()
        {
            //var id = int.Parse(TbId.Text);
            //DELETE ID
            var id = 0;
            var startDate = (DateTime) DpStartDate.SelectedDate;
            var durationInDays = int.Parse(TbDuration.Text);
            var referencePeriod = (CmbPeriod.SelectedIndex == 0) ? Period.daily : Period.weekly;
            var number = int.Parse(TbNumber.Text);
            var selectedMedicine = (Medicine) CmbMedicine.SelectedItem;
            _newPrescription =
                new Prescription(startDate, durationInDays, referencePeriod, number, id, true, selectedMedicine);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
