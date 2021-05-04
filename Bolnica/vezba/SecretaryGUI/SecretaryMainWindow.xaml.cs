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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        public SecretaryMainWindow()
        {
            
            InitializeComponent();
            WindowContent.Content = new SecretaryPatients();
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(206, 208, 253));
            PatientsButton.Background = b;//Brushes.SlateBlue;
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowContent.Content = new SecretaryPatients();
            ResetButtonColors();
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(206, 208, 253));
            PatientsButton.Background = b;
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonColors(); 
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(206, 208, 253));
            AppointmentsButton.Background = b;
        }

        private void ResetButtonColors()
        {
            PatientsButton.Background = Brushes.Transparent;
            AppointmentsButton.Background = Brushes.Transparent;
            DoctorsButton.Background = Brushes.Transparent;
            RoomsButton.Background = Brushes.Transparent;
            AnouncementsButton.Background = Brushes.Transparent;
            PriceListButton.Background = Brushes.Transparent;
        }
    }
}
