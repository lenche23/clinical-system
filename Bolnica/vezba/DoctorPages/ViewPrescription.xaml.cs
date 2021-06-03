﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for ViewPrescription.xaml
    /// </summary>
    public partial class ViewPrescription : Page
    {
        private readonly DoctorView _doctorView;

        public ViewPrescription(Prescription prescription, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = prescription;
            _doctorView = doctorView;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
