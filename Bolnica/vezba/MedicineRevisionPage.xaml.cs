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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicineRevisionPage.xaml
    /// </summary>
    public partial class MedicineRevisionPage : Page
    {

        public Medicine Medicine { get; set; }

        private DoctorView dw;

        public MedicineStorage medStorage;

        public MedicineRevisionPage(Medicine medicine, DoctorView dw)
        {
            InitializeComponent();
            Medicine = medicine;
            DataContext = Medicine;
            this.dw = dw;
            listViewAlergens.ItemsSource = medicine.Ingridient;
        }

        private void ApproveClick(object sender, RoutedEventArgs e)
        {
            Medicine.Status = MedicineStatus.approved;
            medStorage = new MedicineStorage();
            medStorage.Update(Medicine);
            MedicinePageView mpw = new MedicinePageView(dw);
            mpw.MedicineTabs.SelectedIndex = 0;
            dw.Main.Content = mpw;
        }

        private void DeclineClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new DeclineMedicinePage(Medicine, dw);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MedicinePageView mpw = new MedicinePageView(dw);
            mpw.MedicineTabs.SelectedIndex = 0;
            dw.Main.Content = mpw;
        }
    }
}
