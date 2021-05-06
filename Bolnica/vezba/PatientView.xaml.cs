using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace vezba
{
    public partial class PatientView : Window
    {
        public static ObservableCollection<Appointment> Apps { get; set; }

        //public Patient Patient { get; set; }
        public PatientView()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage storage = new AppointmentStorage();
            List<Appointment> apps = storage.GetAll();
            Apps = new ObservableCollection<Appointment>(apps);

            /*PatientStorage pps = new PatientStorage();
            Patient Patient = pps.GetOne("1008985563244");  //ja
            Patient = Patient;*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li Vam je najbitnije da zakazete pregled kod svog lekara?", "Zakazi pregled", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var zakaziPregledLekar = new OrderAppointmentView();
                zakaziPregledLekar.Show();
            }
            else
            {
                var zakaziPregledVreme = new OrderAppointmentTimeView();
                zakaziPregledVreme.Show();
            }
            
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var s = new ViewMyAnnouncements(UserType.patient);
            s.Show();
        }
    }
}
