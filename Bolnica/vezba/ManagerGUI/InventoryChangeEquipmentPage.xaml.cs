using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class InventoryChangeEquipmentPage : Page
    {
        private Equipment equipment;
        private InventoryPage inventoryPage;
        private MainManagerWindow mainManagerWindow;
        public InventoryChangeEquipmentPage(MainManagerWindow mainManagerWindow, Equipment equipment, InventoryPage inventoryPage)
        {
            InitializeComponent();
            this.equipment = equipment;
            this.inventoryPage = inventoryPage;
            DataContext = equipment;
            this.mainManagerWindow = mainManagerWindow;
            List<string> type = new List<string> { "Dinamička", "Statička"};
            comboEquipmentType.ItemsSource = type;
            if (equipment.Type == EquipmentType.dinamical)
            {
                comboEquipmentType.SelectedIndex = 0;
            }
            else
            {
                comboEquipmentType.SelectedIndex = 1;
            }

        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var name = NazivOpreme.Text;
            equipment.Name = name;

            if (comboEquipmentType.SelectedIndex==0)
            {
                equipment.Type = EquipmentType.dinamical;
            }
            else
            {
                equipment.Type = EquipmentType.statical;
            }

            inventoryPage.InventaryBinding.Items.Refresh();
            EquipmentService equipmentService = new EquipmentService();
            equipmentService.UpdateEquipment(equipment);
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

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}