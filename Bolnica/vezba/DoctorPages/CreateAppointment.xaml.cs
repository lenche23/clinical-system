﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
using Calendar = vezba.DoctorPages.Calendar;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateAppointment.xaml
    /// </summary>
    public partial class CreateAppointment : Page
    {
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }
        private DoctorView doctorView;
        private Calendar calendar;

        public CreateAppointment(DoctorView doctorView, Calendar calendar, Doctor doctor)
        {
            InitializeComponent();

            var patientService = new PatientService();
            Patients = patientService.GetAllPatients();

            var doctorService = new DoctorService();
            Doctors = doctorService.GetAllDoctors();

            var roomService = new RoomService();
            Rooms = roomService.GetAllRooms();

            DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            this.doctorView = doctorView;
            this.calendar = calendar;
            if (doctor != null && doctor.Jmbg != null)
                cmbDoctors.SelectedValue = doctor.Jmbg;
        }

        public CreateAppointment(DoctorView doctorView, Calendar calendar, DateTime generatedStartTime, Doctor doctor)
        {
            InitializeComponent();

            var patientService = new PatientService();
            Patients = patientService.GetAllPatients();

            var doctorService = new DoctorService();
            Doctors = doctorService.GetAllDoctors();

            var roomService = new RoomService();
            Rooms = roomService.GetAllRooms();

            DataContext = this;
            cmbPatients.SelectedIndex = 0;
            cmbRooms.SelectedIndex = 0;
            this.doctorView = doctorView;
            this.calendar = calendar;
            StartDatePicker.SelectedDate = generatedStartTime.Date;
            TimeTB.Text = generatedStartTime.ToString("t");
            if (doctor != null && doctor.Jmbg != null)
                cmbDoctors.SelectedValue = doctor.Jmbg;
            DurationTB.Text = "60";
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var newAppointment = NewAppointment();

            var appointmentService = new AppointmentService();
            if (appointmentService.ScheduleAppointment(newAppointment))
            {
                calendar.AddAppointmentToCurrentView(newAppointment);
                doctorView.Main.GoBack();
            }
        }
        private Appointment NewAppointment()
        {
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
            var durationInMinutes = int.Parse(DurationTB.Text);
            var appointmentDescription = DescriptionTB.Text;
            var patient = (Patient) cmbPatients.SelectedItem;
            var room = (Room) cmbRooms.SelectedItem;
            var doctor = (Doctor) cmbDoctors.SelectedItem;
            var isEmergency = (Boolean) IsEmergencyCB.IsChecked;
            var newAppointment = new Appointment(0, patient, doctor, room, startDateTime, durationInMinutes,
                appointmentDescription, isEmergency);
            return newAppointment;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }
    }
}