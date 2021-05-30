﻿using System.Windows;
using System.Windows.Controls;
using Model;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private readonly DoctorView _doctorView;
        private readonly Patient _patient;

        public MedicalRecordPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            _patient = patient;
            DataContext = patient;
            if (patient.MedicalRecord != null)
            {
                _doctorView = doctorView;
            }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private void ExtendButtonClick(object sender, RoutedEventArgs e)
        {
            if (hospitalTreatmentGrid.SelectedItems.Count > 0)
            {
                var hospitalTreatment = (HospitalTreatment) hospitalTreatmentGrid.SelectedItem;
                _doctorView.Main.Content = new ExtendHospitalTreatment(_patient, hospitalTreatment, _doctorView, this);
            }
        }
    }
}
