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
using Model;

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

            EquipmentStorage es = new EquipmentStorage();
            int equipmentID = es.generateNextId();
            var newEquipment = new Equipment(equipmentID, NazivOpreme.Text, type);

            es.Save(newEquipment);
            InventoryPage.EquipmentList.Add(newEquipment);
            NavigationService.GoBack();

        }

        private void Cancel_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            //this.Close();
        }
    }
}
