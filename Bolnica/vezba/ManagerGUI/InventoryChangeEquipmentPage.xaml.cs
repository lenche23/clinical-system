using Model;
using System;
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

            if (equipment.Type == EquipmentType.statical)
            {
                Statička.IsChecked = true;
            }
            else if (equipment.Type == EquipmentType.dinamical)
            {
                Dinamička.IsChecked = true;
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var name = NazivOpreme.Text;
            equipment.Name = name;

            if (Convert.ToBoolean(Statička.IsChecked))
            {
                equipment.Type = EquipmentType.statical;
            }

            else if (Convert.ToBoolean(Dinamička.IsChecked))
            {
                equipment.Type = EquipmentType.dinamical;
            }

            inventoryPage.InventaryBinding.Items.Refresh();
            EquipmentService equipmentService = new EquipmentService();
            equipmentService.UpdateEquipment(equipment);
            NavigationService.GoBack();
        }

        private void Cancel_Change_Button_Click(object sender, RoutedEventArgs e)
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