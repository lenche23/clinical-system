using Model;
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
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for InventaryChangeEquipment.xaml
    /// </summary>
    public partial class InventaryChangeEquipment : Window
    {
        private Equipment equipment;
        private Inventary inventary;
        public InventaryChangeEquipment(Equipment equipment, Inventary inventary)
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
            EquipmentFileRepository es = new EquipmentFileRepository();
            es.Update(equipment);
            this.Close();
        }

        private void Cancel_Change_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}