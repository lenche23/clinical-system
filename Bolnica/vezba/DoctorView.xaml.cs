using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Bolnica;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    /// 

    public partial class DoctorView : Window
    {

        public static ObservableCollection<Appointment> Appointments { get; set; }

        private AppointmentStorage storage;

        private Doctor DoctorUser;

        public static ObservableCollection<Announcement> Ans { get; set; }

        public static ObservableCollection<Medicine> MedicineToApprove { get; set; }

        public static ObservableCollection<Medicine> ApprovedMedicine { get; set; }

        public DoctorView()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new AppointmentStorage();
            List<Appointment> appointments = storage.GetAll();
            Appointments = new ObservableCollection<Appointment>(appointments);
            listViewAppointments.ItemsSource = Appointments;
            DoctorStorage ds = new DoctorStorage();
            DoctorUser = ds.GetOne("1708962324890");

            //--------------------------------------
            UserType ut = UserType.doctor;
            AnnouncementStorage s = new AnnouncementStorage();
            List<Announcement> announcements = s.GetByUser(ut);
            Ans = new ObservableCollection<Announcement>(announcements);

            //---------------------------------------
            MedicineStorage ms = new MedicineStorage();
            List<Medicine> medicineToApprove = ms.GetAwaiting();
            MedicineToApprove = new ObservableCollection<Medicine>(medicineToApprove);
            listViewMedicineRevision.ItemsSource = MedicineToApprove;

            List<Medicine> approvedMedicine = ms.GetApproved();
            ApprovedMedicine = new ObservableCollection<Medicine>(approvedMedicine);
            listViewMedicine.ItemsSource = ApprovedMedicine;

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var s = new CreateAppointmentView();
            s.Show();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                var s = new EditAppointmentView(selected, this);
                s.Show();
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    storage.Delete(selected.AppointentId);
                    Appointments.Remove(selected);
                }
            }
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (listViewAppointments.SelectedItems.Count > 0)
            {
                Appointment selected = (Appointment)listViewAppointments.SelectedItems[0];
                var s = new AppointmentView(selected);
                s.Show();
            }
        }

        private void MedicineClick(object sender, RoutedEventArgs e)
        {
            CalendarView.Visibility = System.Windows.Visibility.Collapsed;
            AnnouncementsView.Visibility = System.Windows.Visibility.Collapsed;
            MedicineView.Visibility = System.Windows.Visibility.Visible;
        }

        private void AnnouncementsClick(object sender, RoutedEventArgs e)
        {
            CalendarView.Visibility = System.Windows.Visibility.Collapsed;
            MedicineView.Visibility = System.Windows.Visibility.Collapsed;
            AnnouncementsView.Visibility = System.Windows.Visibility.Visible;
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                var w = new ViewAnnouncement(a);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }

        private void CalendarClick(object sender, RoutedEventArgs e)
        {
            AnnouncementsView.Visibility = System.Windows.Visibility.Collapsed;
            MedicineView.Visibility = System.Windows.Visibility.Collapsed;
            CalendarView.Visibility = System.Windows.Visibility.Visible;
        }

        private void ViewMedicineClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicine.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicine.SelectedItem;
                var s = new MedicineView(medicine, this);
                s.Show();
            }
        }

        private void RevisionClick(object sender, RoutedEventArgs e)
        {
            if (listViewMedicineRevision.SelectedItems.Count > 0)
            {
                Medicine medicine = (Medicine)listViewMedicineRevision.SelectedItem;
                var s = new RevisionView(medicine);
                s.Show();
            }
        }
    }
}
