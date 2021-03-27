using System.Windows;

namespace vezba
{
    public partial class PatientView : Window
    {
        public PatientView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var zakaziPregled = new OrderAppointmentView();
            zakaziPregled.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var otkaziPregled = new CancelAppointmentView();
            otkaziPregled.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var istorijaPregleda = new AppointmentHistory();
            istorijaPregleda.Show();
        }
    }
}
