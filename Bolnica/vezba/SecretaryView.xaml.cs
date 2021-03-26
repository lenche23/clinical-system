using Bolnica;
using System.Windows;

namespace vezba
{
    public partial class SecretaryView : Window
    {
        public SecretaryView()
        {
            InitializeComponent();
        }

        private void Regestration_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new NewPatient();
            s.Show();
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewPatient();
            s.Show();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new EditPatient();
            s.Show();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
