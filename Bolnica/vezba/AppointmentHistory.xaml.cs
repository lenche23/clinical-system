﻿using Model;
using System;
using System.Collections.Generic;
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
    public partial class AppointmentHistory : Window
    {
        public AppointmentHistory()
        {
            InitializeComponent();
            AppointmentStorage storage = new AppointmentStorage();
            List<Appointment> apps = storage.GetAll();
            lvUsers.ItemsSource = apps;
        }
    }
}