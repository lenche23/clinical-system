using Model;
using System.Windows;
using System.Windows.Controls;

namespace vezba.ManagerGUI
{
    public partial class InventoryViewEquipmentPage : Page
    {
        private Equipment equipment;
        public InventoryViewEquipmentPage(Equipment equipment)
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
        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
