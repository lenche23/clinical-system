using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using Service;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class InventoryChangeEquipmentPage : Page
    {
        private Equipment equipment;
        private InventoryPage inventary;
        public InventoryChangeEquipmentPage(Equipment equipment, InventoryPage inventary)
        {
            InitializeComponent();
            this.equipment = equipment;
            this.inventary = inventary;
            DataContext = equipment;

            if (equipment.Type == EquipmentType.statical)
            {
                Statička.IsChecked = true;
            }
            else if (equipment.Type == EquipmentType.dinamical)
            {
                Dinamička.IsChecked = true;
            }
        }

        private void Okay_Button_Click(object sender, RoutedEventArgs e)
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

            inventary.InventaryBinding.Items.Refresh();
            EquipmentService equipmentService = new EquipmentService();
            equipmentService.UpdateEquipment(equipment);
            NavigationService.GoBack();
        }

        private void Cancel_Change_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}