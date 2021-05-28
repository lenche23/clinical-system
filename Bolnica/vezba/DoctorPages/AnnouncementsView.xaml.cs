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

        public static ObservableCollection<Announcement> Ans { get; set; }

        private DoctorView dw;
        public AnnouncementsView(UserType ut, DoctorView dw)
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            List<Announcement> announcements = s.GetByUser(ut);
            Ans = new ObservableCollection<Announcement>(announcements);
            this.dw = dw;
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                dw.Main.Content = new ViewAnnouncementPage(a, dw);
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}
