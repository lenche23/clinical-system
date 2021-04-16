using Model;
using System;
using System.Collections.Generic;
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

namespace vezba
{
    /// <summary>
    /// Interaction logic for EditAnnouncement.xaml
    /// </summary>
    public partial class EditAnnouncement : Window
    {
        private Announcement announcement;
        public EditAnnouncement(Announcement a)
        {
            InitializeComponent();
            Visibility.Items.Add("Svi");
            Visibility.Items.Add("Zaposleni");
            Visibility.Items.Add("Pacijenti");
            announcement = a;
            if (a.Visibility == Model.Visibility.all)
            {
                Visibility.SelectedIndex = 0;
            }
            else if (a.Visibility == Model.Visibility.staff)
            {
                Visibility.SelectedIndex = 1;
            }
            else
            {
                Visibility.SelectedIndex = 2;
            }
            Content.Text = a.Content;
            Title.Text = a.Title;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementStorage ast = new AnnouncementStorage();
            int id = this.announcement.Id;
            DateTime po = this.announcement.Posted;
            DateTime ed = DateTime.Today;
            String con = Content.Text;
            String tit = Title.Text;
            Model.Visibility vis = Model.Visibility.all;
            if (Visibility.SelectedIndex == 1)
            {
                vis = Model.Visibility.staff;
            }
            if (Visibility.SelectedIndex == 2)
            {
                vis = Model.Visibility.patients;
            }
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            AnnouncementStorage s = new AnnouncementStorage();
            s.Update(announcement);

            var an = Announcements.Ans.FirstOrDefault(a => a.Id == id);
            if (an != null)
            {
                Announcements.Ans[Announcements.Ans.IndexOf(an)] = announcement;
            }

            this.Close();
        }
    }
}
