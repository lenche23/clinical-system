using Bolnica;
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

namespace vezba.ManagerGUI
{

    public partial class RoomChangeEquipmentPage : Page
    {
        private RoomInventory selected;
        private RoomUpdatePage wur;
        private Room r_selected;
        public RoomChangeEquipmentPage(RoomInventory selected, RoomUpdatePage wur, Room r_selected)
        {
            InitializeComponent();
            this.DataContext = selected;
            this.selected = selected;
            this.wur = wur;
            this.r_selected = r_selected;
            NazivOpreme.Content = NazivOpreme.Content + "    " + selected.equipment.Name;
            Id.Content = Id.Content + "    " + selected.equipment.Id;

            if (selected.equipment.Type == EquipmentType.dinamical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Dinamička";
            }
            else if (selected.equipment.Type == EquipmentType.statical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Statička";
            }
        }

        private void PotvrdiIzmenu_Button_Click(object sender, RoutedEventArgs e)
        {
            var Quantity = int.Parse(Količina.Text);
            selected.Quantity = Quantity;

            //RoomFileRepository rs = new RoomFileRepository();
            //rs.UpdateMedicine(this.r_selected);
            RoomInventoryFileRepository ris = new RoomInventoryFileRepository();
            ris.Update(this.selected);
            wur.RoomInventoryBinding.Items.Refresh();
            NavigationService.GoBack();
            //this.Close();

        }

        private void OdustaniIzmena_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            //this.Close();
        }
    }
}

