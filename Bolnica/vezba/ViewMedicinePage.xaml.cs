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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewMedicinePage.xaml
    /// </summary>
    public partial class ViewMedicinePage : Page
    {

        private DoctorView dw;

        private Medicine Medicine { get; set; }

        public ViewMedicinePage(Medicine medicine, DoctorView dw)
        {
            InitializeComponent();
            this.DataContext = medicine;
            listViewAlergens.ItemsSource = medicine.Ingridient;
            this.dw = dw;
            Medicine = medicine;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            dw.Main.Content = new EditMedicinePage(Medicine, this.dw);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            MedicinePageView mpw = new MedicinePageView(dw);
            mpw.MedicineTabs.SelectedIndex = 1;
            dw.Main.Content = mpw;
        }
    }
}
