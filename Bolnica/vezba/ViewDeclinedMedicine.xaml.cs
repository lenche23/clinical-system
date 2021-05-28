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
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewDeclinedMedicine.xaml
    /// </summary>
    public partial class ViewDeclinedMedicine : Page
    {
        private readonly DoctorView _doctorView;

        public DeclinedMedicine DeclinedMedicine { get; set; }

        public ViewDeclinedMedicine(DeclinedMedicine declinedMedicine, DoctorView doctorView)
        {
            InitializeComponent();
            DeclinedMedicine = declinedMedicine;
            _doctorView = doctorView;
            DataContext = declinedMedicine;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
