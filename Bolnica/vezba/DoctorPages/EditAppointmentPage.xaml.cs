﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page, INotifyPropertyChanged
    {
        public Appointment Appointment { get; set; }
        private readonly DoctorView _doctorView;
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Room> Rooms { get; set; }
        private Calendar calendar;
        private Grid appointmentGrid;

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

        private String _description;
        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private String _startTime;

        public String StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }

        public EditAppointmentPage(Appointment appointment, DoctorView doctorView, Calendar calendar, Grid appointmentGrid)
        {
            InitializeComponent();

            var patientService = new PatientService();
            Patients = patientService.GetAllPatients();

            var doctorService = new DoctorService();
            Doctors = doctorService.GetAllDoctors();

            var roomService = new RoomService();
            Rooms = roomService.GetAllRooms();

            Appointment = appointment;
            DataContext = this;
            _doctorView = doctorView;

            if (Appointment.Patient != null && Appointment.Patient.Jmbg != null)
                cmbPatients.SelectedValue = Appointment.Patient.Jmbg;
            if (Appointment.Room != null)
                cmbRooms.SelectedValue = appointment.Room.RoomNumber;
            if (Appointment.Doctor != null && Appointment.Doctor.Jmbg != null)
                cmbDoctors.SelectedValue = Appointment.Doctor.Jmbg;

            this.calendar = calendar;
            this.appointmentGrid = appointmentGrid;

            StartDatePicker.SelectedDate = appointment.StartTime.Date;
            StartTime = Appointment.StartTime.ToString("t");
            Duration = Appointment.DurationInMunutes.ToString();
            Description = Appointment.ApointmentDescription;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if(!ValidateEntries())
                return;
            var updatedAppointment = UpdatedAppointment();

            var appointmentService = new AppointmentService();
            if (appointmentService.DoctorRescheduleAppointment(updatedAppointment))
            {
                calendar.RemoveAppointment(appointmentGrid);
                calendar.AddAppointmentToCurrentView(updatedAppointment);

                _doctorView.Main.GoBack();
                _doctorView.Main.GoBack();
            }
            else
            {
                var newStartTime = appointmentService.FindNextFreeAppointmentStartTime(updatedAppointment);
                StartDatePicker.SelectedDate = newStartTime.Date;
                TimeTB.Text = newStartTime.ToString("t");
            }
        }

        private Appointment UpdatedAppointment()
        {
            var appointmentId = Appointment.AppointentId;
            var startDate = StartDatePicker.SelectedDate;
            var hour = int.Parse(TimeTB.Text.Split(':')[0]);
            var minute = int.Parse(TimeTB.Text.Split(':')[1]);
            var startDateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, hour, minute, 0);
            var durationInMinutes = int.Parse(DurationTB.Text);
            var appointmentDescription = Description;

            var patient = (Patient) cmbPatients.SelectedItem;
            var room = (Room) cmbRooms.SelectedItem;
            var doctor = (Doctor) cmbDoctors.SelectedItem;
            var isEmergency = (Boolean) IsEmergencyCB.IsChecked;
            var updatedAppointment = new Appointment(appointmentId, patient, doctor, room, startDateTime, durationInMinutes,
                appointmentDescription, isEmergency);
            return updatedAppointment;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private Boolean ValidateEntries()
        {
            Boolean ret = true;
            TimeTB.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(TimeTB))
                ret = false;
            DurationTB.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(DurationTB))
                ret = false;
            DescriptionTB.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(DescriptionTB))
                ret = false;
            return ret;
        }
    }
}
