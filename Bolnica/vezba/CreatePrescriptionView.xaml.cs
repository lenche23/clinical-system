﻿using System;
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
using System.Windows.Shapes;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for CreatePrescriptionView.xaml
    /// </summary>
    public partial class CreatePrescriptionView : Window
    {
        public Patient patient;

        public List<Medicine> Medicine { get; set; }
        public MedicineStorage medStorage;

        public CreatePrescriptionView(Patient patient)
        {
            InitializeComponent();
            cmbPeriod.SelectedIndex = 0;
            this.patient = patient;
            medStorage = new MedicineStorage();
            Medicine = medStorage.GetApproved();
            this.DataContext = this;
            CmbMedicine.SelectedIndex = 0;
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

            ObservableCollection<Appointment> appointments = DoctorView.Appointments;
            foreach (Appointment appointment1 in appointments)
            {
                if (appointment1.Patient.Jmbg.Equals(patient.Jmbg))
                {
                    appointment1.Patient.MedicalRecord.AddPrescription(Prescription);
                    aps.Update(appointment1);
                }
            }
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
