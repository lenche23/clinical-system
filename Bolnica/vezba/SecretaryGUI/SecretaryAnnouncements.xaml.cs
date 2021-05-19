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

namespace vezba.SecretaryGUI
{
    public partial class SecretaryAnnouncements : Page
    {
        public static ObservableCollection<Announcement> Announcements { get; set; }
        public SecretaryAnnouncements()
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            List<Announcement> temp = s.GetAll();
            Announcements = new ObservableCollection<Announcement>(temp);
        }

        private void NewAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            SecretaryNewAnnouncement w = new SecretaryNewAnnouncement();
            w.Show();
        }

        private void ViewAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                SecretaryViewAnnouncement w = new SecretaryViewAnnouncement(a);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }

        private void EditAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                SecretaryEditAnnouncement w = new SecretaryEditAnnouncement(a);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }

        private void DeleteAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                AnnouncementFileRepository s = new AnnouncementFileRepository();
                s.Delete(a.Id);
                Announcements.Remove(a);

            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }
    }
}
