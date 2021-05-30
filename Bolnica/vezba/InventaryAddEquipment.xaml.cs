using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using vezba.ManagerGUI;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for InventaryAddEquipment.xaml
    /// </summary>
    public partial class InventaryAddEquipment : Window
    {
        public InventaryAddEquipment()
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

            EquipmentFileRepository es = new EquipmentFileRepository();
            int equipmentID = es.GenerateNextId();
            var newEquipment = new Equipment(equipmentID, NazivOpreme.Text, type);
            
            es.Save(newEquipment);
            InventoryPage.EquipmentList.Add(newEquipment);
            this.Close();
        }

        private void Cancel_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
