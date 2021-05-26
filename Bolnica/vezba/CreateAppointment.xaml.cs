﻿using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Service;
using vezba.Repository;

namespace vezba
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

            PatientService patientService = new PatientService();
            Patients = patientService.GetAll();

            DoctorService doctorService = new DoctorService();
            Doctors = doctorService.GetAll();

            RoomService roomService = new RoomService();
            Rooms = roomService.GetAll();

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

            PatientService patientService = new PatientService();
            Patients = patientService.GetAll();

            DoctorService doctorService = new DoctorService();
            Doctors = doctorService.GetAll();

            RoomService roomService = new RoomService();
            Rooms = roomService.GetAll();

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
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
            var durationInMinutes = int.Parse(DurationTB.Text);
            var appointmentDescription = DescriptionTB.Text;
            var patient = (Patient)cmbPatients.SelectedItem;
            var room = (Room)cmbRooms.SelectedItem;
            var doctor = (Doctor)cmbDoctors.SelectedItem;
            var isEmergency = (Boolean)IsEmergencyCB.IsChecked;
            Appointment newAppointment = new Appointment(0, patient, doctor, room, startDateTime, durationInMinutes, appointmentDescription, isEmergency);

            AppointmentService appointmentService = new AppointmentService();
            appointmentService.Save(newAppointment);

            doctorView.Main.GoBack();

            calendar.AddAppointmentToCurrentView(newAppointment);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }
    }
}
