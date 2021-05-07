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
using System.Windows.Shapes;

namespace vezba
{
    public partial class PatientEditAppointment : Window
    {
        public Appointment Appointment { get; set; }

        public PatientEditAppointment(Appointment appointment)
        {
            InitializeComponent();
            ID.Text = appointment.AppointentId.ToString();
            Datum.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            Opis.Text = appointment.ApointmentDescription;
            Vreme.Text = appointment.StartTime.ToString("HH:mm");
            Trajanje.Text = appointment.DurationInMunutes.ToString();
            Lekar.Text = appointment.Doctor.ToString();
            if (appointment.Room == null)
            {
                Soba.Text = "100";
            }
            else
            {
                Soba.Text = appointment.Room.RoomNumber.ToString();
            }
            Appointment = appointment;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MoveableAppointment()) 
            {
                Appointment appointment = ChangeAppointment();
                AddLog(appointment);
                this.Close();
            }
            else
            {
                MessageBox.Show("Pregled možete pomeriti maksimalno za dva dana.");
            }           
        }

        private Boolean MoveableAppointment()
        {
            DateTime initDate = Appointment.StartTime.Date;
            DateTime endDate = Datum.SelectedDate.Value.Date;
            int diff = (endDate - initDate).Days;
            if (diff > 2)
                return false;

            return true;
        }

        private Appointment ChangeAppointment()
        {
            int id = Appointment.AppointentId;
            DateTime selectedDate = Datum.SelectedDate.Value.Date;
            selectedDate.ToString("MM/dd/yyyy");
            Doctor initDoctor = Appointment.Doctor;
            String selectedTime = Vreme.Text;
            DateTime parsedTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime movedDate = selectedDate.Date.Add(parsedTime.TimeOfDay);

            PatientStorage patientStorage = new PatientStorage();
            Patient patient = patientStorage.GetOne("1008985563244");

            Appointment appointment = new Appointment(initDoctor, movedDate, patient);
            appointment.AppointentId = id;
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointmentStorage.Update(appointment);

            var appointment1 = ChangeAppointmentView.Appointments.FirstOrDefault(a => a.AppointentId.Equals(id));
            if (appointment1 != null)
            {
                ChangeAppointmentView.Appointments[ChangeAppointmentView.Appointments.IndexOf(appointment1)] = appointment;
            }

            return appointment1;
        }

        private void AddLog(Appointment appointment)
        {
            EventsLogStorage eventsLogStorage = new EventsLogStorage();
            List<EventsLog> list = eventsLogStorage.Load();
            String patientJMBG = appointment.Patient.Jmbg;
            List<DateTime> events = new List<DateTime>();
            DateTime log = DateTime.Now;
            foreach (EventsLog elog in list)
            {
                if (elog.PatientJmbg.Equals(patientJMBG))
                {
                    elog.EventDates.Add(log);
                    eventsLogStorage.Update(elog);
                }
            }
        }
    }
}
