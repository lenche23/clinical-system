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
    public partial class OrderAppointmentPopup : Window
    {
        public OrderAppointmentPopup()
        {
            InitializeComponent();
        }

        private void btnVreme_Click(object sender, RoutedEventArgs e)
        {
            var s = new OrderAppointmentTimeView();
            s.Show();
            this.Close();
        }

        private void btnLekar_Click(object sender, RoutedEventArgs e)
        {
            var s = new OrderAppointmentView();
            s.Show();
            this.Close();
        }
    }
}
