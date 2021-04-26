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

namespace vezba
{
    /// <summary>
    /// Interaction logic for WindowChangeItemQuantity.xaml
    /// </summary>
    public partial class WindowChangeItemQuantity : Window
    {
        private Equipment selected;
        private WindowUpdateRoom wur;
        private Room r_selected;
        public WindowChangeItemQuantity(Equipment selected, WindowUpdateRoom wur, Room r_selected)
        {
            InitializeComponent();
            this.selected = selected;
            this.wur = wur;
            this.r_selected = r_selected;
            NazivOpreme.Content = NazivOpreme.Content + "    " + selected.Name;
            Id.Content = Id.Content + "    " + selected.Id;

            if (selected.Type == EquipmentType.dinamical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Dinamička";
            }
            else if (selected.Type == EquipmentType.statical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Statička";
            }
        }

        private void PotvrdiIzmenu_Button_Click(object sender, RoutedEventArgs e)
        {
            var Quantity = int.Parse(Količina.Text);
            selected.Quantity = Quantity;
            RoomStorage rs = new RoomStorage();
            rs.Update(this.r_selected);
            wur.EquipmentBinding.Items.Refresh();
            this.Close();

        }

        private void OdustaniIzmena_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
