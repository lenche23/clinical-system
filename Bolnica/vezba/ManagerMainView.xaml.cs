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
    /// Interaction logic for ManagerMainView.xaml
    /// </summary>
    public partial class ManagerMainView : Window
    {
        public ManagerMainView()
        {
            InitializeComponent();
        }

        private void Inventary_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new Inventary();
            s.Show();
        }

        private void Medicine_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddMedicineWindow();
            s.Show();

        }

        private void Rooms_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new ManagerView();
            s.Show();
        }

        private void Announcements_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewMyAnnouncements(UserType.menager);
            s.Show();
        }
    }
}
