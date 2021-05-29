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

        public ViewAnnouncementPage(Announcement a, DoctorView doctorView)
        {
            InitializeComponent();
            Posted.Content += a.FormatedDatePosted;
            Edited.Content += a.FormatedDateEdited;
            if (Posted.Content.Equals(Edited.Content))
            {
                Edited.Visibility = System.Windows.Visibility.Collapsed;
            }
            Content.Text = a.Content;
            Title.Text = a.Title;
            _doctorView = doctorView;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
