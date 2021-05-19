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
using vezba.Repository;

namespace vezba.PatientPages
{
    public partial class NotificationsPage : Page
    {
        public static ObservableCollection<Announcement> Ans { get; set; }
        public NotificationsPage(UserType ut, String jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            List<Announcement> announcements = s.GetByUser(ut);
            announcements.AddRange(s.getIndividualAnnouncements(jmbg));

            Ans = new ObservableCollection<Announcement>(announcements);
        }

        private void ButtonShowNotification_Click(object sender, RoutedEventArgs e)
        {
            if (announcementsTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementsTable.SelectedItem;
                this.NavigationService.Navigate(new ShowNotificationPage(a));
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
            
        }
    }
}
