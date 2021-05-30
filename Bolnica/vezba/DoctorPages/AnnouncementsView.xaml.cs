using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using vezba.Repository;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for AnnouncementsView.xaml
    /// </summary>
    public partial class AnnouncementsView : Page
    {

        public static ObservableCollection<Announcement> Announcements { get; set; }

        private readonly DoctorView _doctorView;
        public AnnouncementsView(UserType userType, DoctorView doctorView)
        {
            InitializeComponent();
            DataContext = this;
            var announcementFileRepository = new AnnouncementFileRepository();
            var announcements = announcementFileRepository.GetByUser(userType);
            Announcements = new ObservableCollection<Announcement>(announcements);
            _doctorView = doctorView;
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                var announcement = (Announcement)announcementTable.SelectedItem;
                _doctorView.Main.Content = new ViewAnnouncementPage(announcement, _doctorView);
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}
