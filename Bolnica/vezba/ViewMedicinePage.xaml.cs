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

        private readonly DoctorView _doctorView;

        private Medicine Medicine { get; set; }

        private readonly MedicinePageView _medicinePageView;

        public ViewMedicinePage(Medicine medicine, DoctorView doctorView, MedicinePageView medicinePageView)
        {
            InitializeComponent();
            DataContext = medicine;
            _doctorView = doctorView;
            _medicinePageView = medicinePageView;
            Medicine = medicine;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new EditMedicinePage(Medicine, _doctorView, _medicinePageView);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
