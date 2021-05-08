using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using vezba.PatientPages;

namespace vezba
{
    public partial class PatientView : Window
    {
        public static ObservableCollection<Appointment> Apps { get; set; }
        public static Patient Patient { get; set; }
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
            var medicine1 = new Medicine("Vitamin C", "nesto", "nesto", 0, MedicineCondition.pill);
            Prescription p1 = new Prescription(DateTime.Now, 21, Period.daily, 1, 0, true, medicine1);
            var medicine2 = new Medicine("Brufen", "nesto", "nesto", 0, MedicineCondition.pill);
            Prescription p2 = new Prescription(DateTime.Now, 21, Period.daily, 2, 1, true, medicine2);
            MedicalRecord medicalRecord = new MedicalRecord("12345", "12345");
            Patient.MedicalRecord = medicalRecord;
            Patient.MedicalRecord.AddPrescription(p1);
            Patient.MedicalRecord.AddPrescription(p2);

            PatientMainPage.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public static void medicineNotification()
        {
            while (true)
            {
                foreach (Prescription p in Patient.MedicalRecord.prescription)
                {
                    GenerateNotificationsSpanTime(p);
                }
                Thread.Sleep(TimeSpan.FromMinutes(5));
            }
        }

        public static List<DateTime> GenerateNotificationsSpanTime(Prescription prescription)
        {
            DateTime start = prescription.StartDate;
            DateTime end = prescription.StartDate.AddDays(prescription.DurationInDays);

            DateTime it = new DateTime();
            it = prescription.StartDate.AddSeconds(0);
            List<DateTime> notifications = new List<DateTime>();

            while (it.Date < end.Date)
            {
                if (prescription.ReferencePeriod == Period.daily)
                {
                    for (int i = 0; i < prescription.Number; i++)
                    {
                        notifications.Add(start.AddHours(i * 24 / prescription.Number));
                    }
                    it = it.AddDays(1);
                }

                if (prescription.ReferencePeriod == Period.weekly)
                {
                    for (int i = 0; i < prescription.Number; i++)
                    {
                        notifications.Add(start.AddDays(i * 7 / prescription.Number));
                    }
                    it = it.AddDays(7);
                }

            }

            foreach (DateTime dt in notifications)
            {
                if (dt >= DateTime.Now && dt <= DateTime.Now.AddMinutes(5))
                {
                    MessageBox.Show("Podsetnik da lek " + prescription.Medicine.Name + "trebate popiti u " + dt.ToString("HH:mm"), "Terapija", MessageBoxButton.OK);
                    /*PatientNotifications noti = new PatientNotifications("Terapija", "Podsetnik da lek " + prescription.Medicine.Name + "trebate popiti u " + dt.ToString("HH:mm"));
                    noti.Show();*/
                }
            }

            return notifications;
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
            var s = new LogoutPopup(this);
            s.Show();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatientMainPage.NavigationService.GoBack();
            }
            catch (Exception ex)
            {

            }
            
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

        private void ButtonMore_Click(object sender, RoutedEventArgs e)
        {
            PatientMainPage.Navigate(new MorePage());
        }
    }
}
