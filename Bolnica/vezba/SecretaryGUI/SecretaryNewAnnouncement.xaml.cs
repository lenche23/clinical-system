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
    public partial class SecretaryNewAnnouncement : Window
    {
        public SecretaryNewAnnouncement()
        {
            InitializeComponent();

            this.DataContext = this;
            PatientFileRepository ps = new PatientFileRepository();
            List<Patient> patients = ps.GetAll();
            RecipientComboBox.ItemsSource = patients;

            VisibilityComboBox.Items.Add("Svi");
            VisibilityComboBox.Items.Add("Pacijenti");
            VisibilityComboBox.Items.Add("Zaposleni");
            VisibilityComboBox.Items.Add("Lekari");
            VisibilityComboBox.Items.Add("Upravnik");
            VisibilityComboBox.Items.Add("Sekretari");

            VisibilityComboBox.Items.Add("Individualno");
            VisibilityComboBox.SelectedIndex = -1;

            RecipientLabel.Visibility = System.Windows.Visibility.Collapsed;
            RecipientComboBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (VisibilityComboBox.SelectedIndex == 6)
            {
                RecipientLabel.Visibility = System.Windows.Visibility.Visible;
                RecipientComboBox.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                RecipientLabel.Visibility = System.Windows.Visibility.Collapsed;
                RecipientComboBox.Visibility = System.Windows.Visibility.Collapsed;
            }
                
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementFileRepository ast = new AnnouncementFileRepository();
            int id = ast.generateNextId();
            DateTime po = DateTime.Today;
            DateTime ed = DateTime.Today;
            String con = Content.Text;
            String tit = Title.Text;
            Patient recipient = (Patient)RecipientComboBox.SelectedItem;
            Model.Visibility vis = Model.Visibility.all;
            if (VisibilityComboBox.SelectedIndex == 1)
                vis = Model.Visibility.patients;
            else if (VisibilityComboBox.SelectedIndex == 2)
                vis = Model.Visibility.staff;
            else if (VisibilityComboBox.SelectedIndex == 3)
                vis = Model.Visibility.doctors;
            else if (VisibilityComboBox.SelectedIndex == 4)
                vis = Model.Visibility.menagers;
            else if (VisibilityComboBox.SelectedIndex == 5)
                vis = Model.Visibility.secretaries;
            else if (VisibilityComboBox.SelectedIndex == 6)
            {
                vis = Model.Visibility.individual;
                if (RecipientComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Niste uneli primaoca!");
                    return;
                }
            }
                
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            if(vis == Model.Visibility.individual)
                announcement.AddRecipient(recipient.Jmbg);
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            s.Save(announcement);
            SecretaryAnnouncements.Announcements.Add(announcement);

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
