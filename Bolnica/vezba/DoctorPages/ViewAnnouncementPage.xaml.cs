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
        private DoctorView dw;

        public ViewAnnouncementPage(Announcement a, DoctorView dw)
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
            this.dw = dw;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
