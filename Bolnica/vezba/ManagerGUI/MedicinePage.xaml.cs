﻿using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Service;

namespace vezba.ManagerGUI
{
    public partial class MedicinePage : Page
    {
        public static ObservableCollection<Medicine> MedicineList { get; set; }
        public static List<Medicine> medicineList;
        private MainManagerWindow mainManagerWindow;
        public MedicinePage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            MedicineService medicineService = new MedicineService();
            medicineList = medicineService.GetAllMedicine();
            MedicineList = new ObservableCollection<Medicine>(medicineList);
            MedicineBinding.ItemsSource = MedicineList;
        }

        private void Add_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicineAddPage();
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void Declined_Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new DeclinedMedicineManagerPage(mainManagerWindow, this);
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MedicineBinding.SelectedIndex > -1)
            {
                Medicine medicine = (Medicine)MedicineBinding.SelectedItems[0];
                mainManagerWindow.MainManagerView.Content = new MedicineUpdatePage(mainManagerWindow, medicine, this);
            }

            else
            {
                MessageBox.Show("Ni jedan lek nije selektovan!");
            }
        }
    }
}
