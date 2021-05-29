using System.Windows;
using System.Windows.Controls;
using Model;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for ViewAnnouncementPage.xaml
    /// </summary>
    public partial class ViewAnnouncementPage : Page
    {
        private readonly DoctorView _doctorView;

        public ViewAnnouncementPage(Announcement announcement, DoctorView doctorView)
        {
            InitializeComponent();
            Posted.Content += announcement.FormatedDatePosted;
            Edited.Content += announcement.FormatedDateEdited;
            if (Posted.Content.Equals(Edited.Content))
            {
                Edited.Visibility = System.Windows.Visibility.Collapsed;
            }
            Content.Text = announcement.Content;
            Title.Text = announcement.Title;
            _doctorView = doctorView;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
