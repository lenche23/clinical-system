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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Service;
using vezba.Repository;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewAppointmentPage.xaml
    /// </summary>
    public partial class ViewAppointmentPage : Page
    {

        private Appointment Selected { get; set; }
        private DoctorView doctorView;
        private Calendar calendar;
        private Grid appointmentGrid;

        public ViewAppointmentPage(Appointment selected, DoctorView doctorView, Calendar calendar, Grid appointmentGrid)
        {
            InitializeComponent();
            DataContext = selected;
            Selected = selected;
            this.doctorView = doctorView;
            this.calendar = calendar;
            this.appointmentGrid = appointmentGrid;
        }

        private void MedicalRecordClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.Content = new MedicalRecordPage(Selected.Patient, doctorView);
        }

        private void NewAnamnesisClick(object sender, RoutedEventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, Selected.StartTime) >= 0)
            {
                doctorView.Main.Content = new CreateAnamnesisPage(Selected, doctorView);
            }
            else
            {
                MessageBox.Show("Nije moguće kreirati izveštaj pre termina pregleda!");
            }
        }

        private void NewPrescriptionClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.Content = new CreatePrescriptionPage(Selected.Patient, doctorView);
        }

        private void NewReferralLetterClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.Content = new CreateReferralLetterPage(Selected.Patient, doctorView);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.GoBack();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.Content = new EditAppointmentPage(Selected, doctorView, calendar, appointmentGrid);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                AppointmentService appointmentService = new AppointmentService();
                appointmentService.Delete(Selected.AppointentId);
                calendar.RemoveAppointment(appointmentGrid);
                calendar.SetScrollViewerToFirstAppointment();
                doctorView.Main.GoBack();
            }
        }
    }
}
