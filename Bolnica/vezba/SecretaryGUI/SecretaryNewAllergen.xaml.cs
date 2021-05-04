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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryNewAllergen.xaml
    /// </summary>
    public partial class SecretaryNewAllergen : Window
    {
        public int Mode { get; set; }
        public SecretaryNewAllergen(int mode)
        {
            InitializeComponent();
            Mode = mode;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Ingridient ingridient = new Ingridient(Name.Text);
            if (Mode == 0)
            {
                SecretaryNewPatient.Allergens.Add(ingridient);
            }
            else if (Mode == 1)
            {
                SecretaryEditPatient.Allergens.Add(ingridient);
            }
            this.Close();
            return;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
