using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace vezba
{
    public partial class PatientView : Window
    {
        public static ObservableCollection<Appointment> Apps { get; set; }
        public PatientView()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage storage = new AppointmentStorage();
            List<Appointment> apps = storage.GetAll();
            Apps = new ObservableCollection<Appointment>(apps);
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var izmeniPregled = new ChangeAppointmentView();
            izmeniPregled.Show();
        }
    }
}
