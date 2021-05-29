using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class MorePage : Page
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public static int currentAppointments = 0;

        public MorePage()
        {
            InitializeComponent();
            AppointmentFileRepository ps = new AppointmentFileRepository();
            List<Appointment> temp = new List<Appointment>();
            foreach (Appointment appointment in ps.GetAll())
            {
                if (appointment.StartTime.Date < DateTime.Now && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    temp.Add(appointment);

                }
            }
            Appointments = new ObservableCollection<Appointment>(temp);
            currentAppointments = Appointments.Count();
        }

        private void BtnZahtev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOcenaLekara_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GradeDoctorPage());
        }

        private void BtnOcenaBolnice_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("aaaa", "nesto" + currentAppointments);
            if (currentAppointments % 5 == 0)
            {
                this.NavigationService.Navigate(new GradeHospitalPage());
            }
            else
            {
                var s = new GradePopup();
                s.Show();
            }
        }

        private void BtnStatistika_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMojLekar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPlacanje_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnIzvestaj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
