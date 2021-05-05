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
        public Patient patient;

        public MedicineStorage medStorage;
        private DoctorView dw;
        public List<Medicine> FilteredMedicine { get; set; }

        public CreatePrescriptionPage(Patient patient, DoctorView dw)
        {
            InitializeComponent();
            cmbPeriod.SelectedIndex = 0;
            this.patient = patient;
            medStorage = new MedicineStorage();
            List<Medicine> Medicine = medStorage.GetApproved();
            DataContext = this;
            CmbMedicine.SelectedIndex = 0;
            this.dw = dw;
            Boolean matches = false;
            FilteredMedicine = new List<Medicine>();
            foreach(Medicine med in Medicine)
            {
                foreach(Ingridient ing in med.ingridient)
                {
                    foreach(Ingridient al in patient.MedicalRecord.Allergen)
                    {
                        if(ing.Name.Equals(al.Name))
                        {
                            matches = true;
                        }
                    }
                }
                if (!matches)
                    FilteredMedicine.Add(med);
                matches = false;
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse(TbId.Text);
            var StartDate = DpStartDate.SelectedDate;
            var DurationInDays = int.Parse(TbDuration.Text);
            Period ReferencePeriod;
            if (cmbPeriod.SelectedIndex == 0)
                ReferencePeriod = Period.daily;
            else
                ReferencePeriod = Period.weekly;
            var Number = int.Parse(TbNumber.Text);
            var SelectedMedicine = CmbMedicine.SelectedItem;
            var Prescription = new Prescription(Id, (DateTime)StartDate, DurationInDays, ReferencePeriod, Number, (Medicine)SelectedMedicine);

            patient.MedicalRecord.AddPrescription(Prescription);
            PatientStorage ps = new PatientStorage();
            ps.Update(patient);

            AppointmentStorage aps = new AppointmentStorage();
            List<Appointment> appointments = aps.GetAll();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment.Patient.MedicalRecord.AddPrescription(Prescription);
                    aps.Update(appointment);
                }
            }
            foreach (Appointment appointment in CalendarView.Appointments)
            {
                if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment.Patient.MedicalRecord.AddPrescription(Prescription);
                }
            }
            dw.Main.GoBack();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }

    }
}
