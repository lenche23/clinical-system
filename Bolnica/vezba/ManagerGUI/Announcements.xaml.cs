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
using System.Windows.Shapes;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class Announcements : Page
    {

        public static ObservableCollection<Announcement> Ans { get; set; }
        private MainManagerWindow mainManagerWindow;
        public Announcements(MainManagerWindow mainManagerWindow, UserType ut)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            List<Announcement> announcements = s.GetByUser(ut);
            Ans = new ObservableCollection<Announcement>(announcements);
        }

        public Announcements(UserType ut, String jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            /*List<Announcement> announcementsForUserType = s.GetByUser(ut);
            List<Announcement> individualAnnouncements = s.getIndividualAnnouncements(jmbg);*/
            List<Announcement> announcements = s.GetByUser(ut);
            announcements.AddRange(s.getIndividualAnnouncements(jmbg));

            Ans = new ObservableCollection<Announcement>(announcements);
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement) announcementTable.SelectedItem;
                mainManagerWindow.MainManagerView.Content = new ViewAnnouncementManagerPage(a);
                //var w = new ViewAnnouncement(a);
                //w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}
