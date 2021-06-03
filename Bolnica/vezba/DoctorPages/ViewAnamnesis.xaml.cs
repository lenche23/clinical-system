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

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for ViewAnamnesis.xaml
    /// </summary>
    public partial class ViewAnamnesis : Page
    {
        private readonly DoctorView _doctorView;

        public ViewAnamnesis(Anamnesis anamnesis, DoctorView doctorView)
        {
            InitializeComponent();
            _doctorView = doctorView;
            DataContext = anamnesis;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
