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

namespace vezba.PatientPages
{
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (135 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridTable.Background = Brushes.Aqua;
                    break;
                case 1:
                    GridTable.Background = Brushes.Blue;
                    break;
                case 2:
                    GridTable.Background = Brushes.Violet;
                    break;
            }
        }
    }
}
