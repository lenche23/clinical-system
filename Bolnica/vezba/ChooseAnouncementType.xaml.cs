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
    public partial class ChooseAnouncementType : Window
    {
        public ChooseAnouncementType()
        {
            InitializeComponent();
        }


        private void General_Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewAnnouncement();
            w.Show();
            this.Close();
        }

        private void Individual_Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewIndividualAnouncement();
            w.Show();
            this.Close();
        }
    }
}
