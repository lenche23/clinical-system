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

namespace vezba.ManagerGUI
{
    /// <summary>
    /// Interaction logic for MainManagerPage.xaml
    /// </summary>
    public partial class MainManagerPage : Page
    {
        private MainManagerWindow mainManagerWindow;
        public MainManagerPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
        }

        private void ButtonAnnouncementsClick(object sender, RoutedEventArgs e)
        {
            var s = new ViewMyAnnouncements(UserType.menager);
            s.Show();
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage();
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage();
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage();
        }

        private void ButtonStaffClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSupplyClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSurveyClick(object sender, RoutedEventArgs e)
        {

        }
    }
}