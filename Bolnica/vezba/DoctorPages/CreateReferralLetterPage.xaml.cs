﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateReferralLetterPage.xaml
    /// </summary>
    public partial class CreateReferralLetterPage : Page
    {
        public List<Doctor> Doctors { get; set; }

        private readonly Patient _patient;

        private readonly DoctorView _doctorView;

        public CreateReferralLetterPage(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = patient;

            var doctorService = new DoctorService();
            Doctors = doctorService.GetAllDoctors();

            cmbDoctors.DataContext = this;
            cmbDoctors.SelectedIndex = 0;
            _patient = patient;
            _doctorView = doctorView;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        { 
            var newReferralLetter = NewReferralLetter();

            var patientService = new PatientService();
            patientService.AddReferralLetterToPatient(_patient, newReferralLetter);

            _doctorView.Main.GoBack();
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