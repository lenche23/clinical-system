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
using vezba.Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewDeclinedMedicine.xaml
    /// </summary>
    public partial class ViewDeclinedMedicine : Page
    {
        private DoctorView dw;

        public DeclinedMedicine DeclinedMedicine { get; set; }

        public ViewDeclinedMedicine(DeclinedMedicine DeclinedMedicine, DoctorView dw)
        {
            InitializeComponent();
            this.DeclinedMedicine = DeclinedMedicine;
            this.dw = dw;
            listViewAlergens.ItemsSource = DeclinedMedicine.Medicine.ingridient;
            DataContext = DeclinedMedicine;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
