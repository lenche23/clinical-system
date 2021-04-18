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
        public WindowChangeItemQuantity(Equipment selected, WindowUpdateRoom wur)
        {
            InitializeComponent();
            this.selected = selected;
            this.wur = wur;
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
            wur.EquipmentBinding.Items.Refresh();
            this.Close();

        }

        private void OdustaniIzmena_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
