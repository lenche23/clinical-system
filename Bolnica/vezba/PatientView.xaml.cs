using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using vezba.PatientPages;

namespace vezba
{
    public partial class PatientView : Window
    {
        public static ObservableCollection<Appointment> Apps { get; set; }
        public Patient Patient { get; set; }
        public PatientView()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage storage = new AppointmentStorage();
            List<Appointment> apps = storage.GetAll();
            Apps = new ObservableCollection<Appointment>(apps);

            PatientStorage pps = new PatientStorage();
            Patient patient = pps.GetOne("1008985563244");  //ja
            Patient = patient;
            PatientMainPage.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        /*private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonCloseMenu.Visibility = Visibility.Visible;
            //ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonOpenMenu.Visibility = Visibility.Visible;
            //ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }*/

        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            PatientMainPage.Content = new MainPage();
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            var s = new LogoutPopup();
            s.Show();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            PatientMainPage.NavigationService.GoBack();
        }

        private void Appointments_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientMainPage.Navigate(new AppointmentsPage());
        }

        private void Therapies_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientMainPage.Navigate(new TherapiesPage());
        }

        private void Events_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientMainPage.Navigate(new CalendarPage());
        }

        private void Notifications_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientMainPage.Navigate(new NotificationsPage(UserType.patient));
        }
    }
}
