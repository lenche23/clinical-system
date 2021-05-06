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

namespace vezba
{
    /// <summary>
    /// Interaction logic for InventaryViewEquipment.xaml
    /// </summary>
    public partial class InventaryViewEquipment : Window
    {
        private Equipment equipment;
        public InventaryViewEquipment(Equipment equipment)
        {
            InitializeComponent();
            this.equipment = equipment;

            NazivOpreme.Content = NazivOpreme.Content + " " + equipment.Name;

            if (equipment.Type == EquipmentType.dinamical)
            {
                TipOpreme.Content = TipOpreme.Content + " Dinamička";
            }
            else if (equipment.Type == EquipmentType.statical)
            {
                TipOpreme.Content = TipOpreme.Content + " Statička";
            }

        }
    }
}
