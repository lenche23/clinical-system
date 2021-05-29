using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class InventoryAddEquipmentPage : Page
    {
        public InventoryAddEquipmentPage()
        {
            InitializeComponent();
        }

        private void Approve_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentType type = EquipmentType.statical;

            if (Convert.ToBoolean(Statička.IsChecked))
            {
                type = EquipmentType.statical;
            }

            else if (Convert.ToBoolean(Dinamička.IsChecked))
            {
                type = EquipmentType.dinamical;
            }


            EquipmentService equipmentService = new EquipmentService();
            int equipmentID = equipmentService.generateNextEquipmentId();
            var newEquipment = new Equipment(equipmentID, NazivOpreme.Text, type);

            equipmentService.SaveEquipment(newEquipment);
            InventoryPage.EquipmentList.Add(newEquipment);
            NavigationService.GoBack();

        }

        private void Cancel_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
