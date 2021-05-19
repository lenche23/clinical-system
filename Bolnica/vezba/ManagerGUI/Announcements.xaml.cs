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
        public Announcements()
        {
            InitializeComponent();
            this.DataContext = this;
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            List<Announcement> temp = s.GetAll();
            Ans = new ObservableCollection<Announcement>(temp);
        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new ChooseAnouncementType();
            w.Show();
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                var w = new ViewAnnouncement(a);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                var w = new EditAnnouncement(a);
                w.Show();
            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (announcementTable.SelectedCells.Count > 0)
            {
                Announcement a = (Announcement)announcementTable.SelectedItem;
                AnnouncementFileRepository s = new AnnouncementFileRepository();
                s.Delete(a.Id);
                Ans.Remove(a);

            }
            else
            {
                MessageBox.Show("Niste selektovali obavestenje!");
            }
        }
    }
}

