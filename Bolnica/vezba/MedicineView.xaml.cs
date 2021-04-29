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
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MedicineView.xaml
    /// </summary>
    public partial class MedicineView : Window
    {

        public Medicine medicine;

        public DoctorView view;

        public MedicineView(Medicine medicine, DoctorView view)
        {
            InitializeComponent();
            this.DataContext = medicine;
            listViewAlergens.ItemsSource = medicine.Ingridient;
            this.medicine = medicine;
            this.view = view;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var s = new EditMedicineView(medicine, view);
            this.Close();
            s.Show();
        }
    }
}
