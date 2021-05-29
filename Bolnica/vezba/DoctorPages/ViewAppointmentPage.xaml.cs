using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;
using Calendar = vezba.DoctorPages.Calendar;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for ViewAppointmentPage.xaml
    /// </summary>
    public partial class ViewAppointmentPage : Page
    {

        private Appointment Selected { get; set; }
        private readonly DoctorView _doctorView;
        private Calendar calendar;
        private Grid appointmentGrid;

        public ViewAppointmentPage(Appointment selected, DoctorView doctorView, Calendar calendar, Grid appointmentGrid)
        {
            InitializeComponent();
            DataContext = selected;
            Selected = selected;
            _doctorView = doctorView;
            this.calendar = calendar;
            this.appointmentGrid = appointmentGrid;
        }

        private void MedicalRecordClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new MedicalRecordPage(Selected.Patient, _doctorView);
        }

        private void NewAnamnesisClick(object sender, RoutedEventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, Selected.StartTime) >= 0)
            {
                _doctorView.Main.Content = new CreateAnamnesisPage(Selected, _doctorView);
            }
            else
            {
                MessageBox.Show("Nije moguće kreirati izveštaj pre termina pregleda!");
            }
        }

        private void NewPrescriptionClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new CreatePrescriptionPage(Selected.Patient, _doctorView);
        }

        private void NewReferralLetterClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new CreateReferralLetterPage(Selected.Patient, _doctorView);
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.Content = new EditAppointmentPage(Selected, _doctorView, calendar, appointmentGrid);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "DeleteMedicine Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var appointmentService = new AppointmentService();
                appointmentService.DeleteAppointment(Selected.AppointentId);
                calendar.RemoveAppointment(appointmentGrid);
                calendar.SetScrollViewerToFirstAppointment();
                _doctorView.Main.GoBack();
            }
        }
    }
}
