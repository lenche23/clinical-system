﻿using Model;
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

namespace vezba.PatientPages
{
    public partial class MainPage : Page
    {
        public static ObservableCollection<Appointment> Apps { get; set; }
        public Patient Patient { get; set; }
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage storage = new AppointmentStorage();
            List<Appointment> apps = storage.GetAll();
            Apps = new ObservableCollection<Appointment>(apps);

            PatientStorage pps = new PatientStorage();
            Patient patient = pps.GetOne("1008985563244");  //ja
            Patient = patient;
        }

    }
}