using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Service;
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
            AnnouncementService announcementService = new AnnouncementService();
            List<Announcement> announcements = announcementService.GetManagerAnnouncementsByUser(ut);
            Ans = new ObservableCollection<Announcement>(announcements);
        }

        public Announcements(UserType ut, String jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
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
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}
