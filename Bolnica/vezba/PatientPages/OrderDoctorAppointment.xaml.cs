﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class OrderDoctorAppointment : Page
    {
        private DoctorService DoctorService { get; set; }
        private AppointmentService AppointmentService { get; set; }
        public Doctor Doctor{ get; set; }
        public OrderDoctorAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorService = new DoctorService();
            AppointmentService = new AppointmentService();
            Doctor = DoctorService.LoadDoctor();
        }

        private void OrderAppointment_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = calendar.SelectedDate.Value.Date;
            String selectedTime = comboBox.Text;

            if (HasAllInfo())
            {
                DateTime dateTimeFinal = AppointmentService.ParseTime(selectedDate, selectedTime);
                Appointment a = new Appointment(Doctor, dateTimeFinal, PatientView.Patient);
               
                if (AppointmentService.CanSchedule(selectedDate))
                {
                    AppointmentService.PatientCanScheduleAppointment(a);
                }
                else
                {
                    PatientNotification noti = new PatientNotification("Unet datum nije validan!");
                    noti.ShowDialog();
                }
            }
            else
            {
                PatientNotification noti = new PatientNotification("Molimo Vas popunite sva potrebna polja!");
                noti.ShowDialog();
            }
        }

        private Boolean HasAllInfo()
        {
            if (calendar.SelectedDate != null && (comboBox.Text != null && !comboBox.Text.Equals("")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}