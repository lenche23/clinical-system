using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.ManagerGUI
{
    public partial class InventoryAddEquipmentPage : Page
    {
        private MainManagerWindow mainManagerWindow;
        public InventoryAddEquipmentPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            List<string> type = new List<string> { "Dinamička", "Statička" };
            comboEquipmentType.ItemsSource = type;
        }
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            EquipmentType type = EquipmentType.statical;

            if (comboEquipmentType.SelectedIndex == 0)
            {
               type = EquipmentType.dinamical;
            }
            else
            {
               type = EquipmentType.statical;
            }
            EquipmentService equipmentService = new EquipmentService();
            var newEquipment = new Equipment(0, NazivOpreme.Text, type);
            equipmentService.SaveEquipment(newEquipment);
            InventoryPage.EquipmentList.Add(newEquipment);
            NavigationService.GoBack();
        }
        private void Cancel_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }
    }
}
