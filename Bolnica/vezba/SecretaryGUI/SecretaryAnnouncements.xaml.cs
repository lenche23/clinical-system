using Model;
using Service;
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
            AnnouncementService announcementService = new AnnouncementService();
            List<Announcement> temp = announcementService.GetAllAnnouncements();
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
                Announcement announcement = (Announcement)announcementTable.SelectedItem;
                SecretaryViewAnnouncement w = new SecretaryViewAnnouncement(announcement);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }

        private void EditAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement announcement = (Announcement) announcementTable.SelectedItem;
                SecretaryEditAnnouncement w = new SecretaryEditAnnouncement(announcement);
                w.Show();
            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }

        private void DeleteAnnouncementButton_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement selecetedAnnouncement = (Announcement)announcementTable.SelectedItem;
                AnnouncementService announcementService = new AnnouncementService();
                announcementService.DeleteAnnouncement(selecetedAnnouncement.Id);
                Announcements.Remove(selecetedAnnouncement);

            }
            else
                MessageBox.Show("Niste selektovali obavestenje!");
        }
    }
}
