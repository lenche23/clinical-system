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
using vezba.Repository;

namespace vezba.SecretaryGUI
{

    public partial class SecretaryEditAnnouncement : Window
    {
        private Announcement announcement;
        public SecretaryEditAnnouncement(Announcement a)
        {
            InitializeComponent();
            announcement = a;
            RecipientLabel.Visibility = System.Windows.Visibility.Collapsed;
            RecipientTitleLabel.Visibility = System.Windows.Visibility.Collapsed;
            switch (a.Visibility)
            {
                case Model.Visibility.all:
                    VisibilityLabel.Content = "Svi";
                    break;
                case Model.Visibility.doctors:
                    VisibilityLabel.Content = "Lekari";
                    break;
                case Model.Visibility.secretaries:
                    VisibilityLabel.Content = "Sekretari";
                    break;
                case Model.Visibility.menagers:
                    VisibilityLabel.Content = "Upravnici";
                    break;
                case Model.Visibility.staff:
                    VisibilityLabel.Content = "Zaposleni";
                    break;
                case Model.Visibility.patients:
                    VisibilityLabel.Content = "Pacijenti";
                    break;
                case Model.Visibility.individual:
                    VisibilityLabel.Content = "Individualno";
                    RecipientLabel.Visibility = System.Windows.Visibility.Visible;
                    RecipientTitleLabel.Visibility = System.Windows.Visibility.Visible;
                    //Patient recipient = patientsStorage.GetOne(a.Recipients[0]);
                    PatientFileRepository patientFileRepository = new PatientFileRepository();
                    RecipientLabel.Content = patientFileRepository.GetOne(a.Recipients[0]).NameAndSurname;
                    break;
            }

            Content.Text = a.Content;
            Title.Text = a.Title;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementFileRepository ast = new AnnouncementFileRepository();
            int id = this.announcement.Id;
            DateTime po = this.announcement.Posted;
            DateTime ed = DateTime.Today;
            String con = Content.Text;
            String tit = Title.Text;
            Model.Visibility vis = this.announcement.Visibility;
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            if (vis == Model.Visibility.individual)
                announcement.AddRecipient(this.announcement.Recipients[0]);

            AnnouncementFileRepository s = new AnnouncementFileRepository();
            s.Update(announcement);

            var an = SecretaryAnnouncements.Announcements.FirstOrDefault(a => a.Id == id);
            if (an != null)
            {
                SecretaryAnnouncements.Announcements[SecretaryAnnouncements.Announcements.IndexOf(an)] = announcement;
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
