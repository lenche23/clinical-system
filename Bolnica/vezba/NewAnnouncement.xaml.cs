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

namespace vezba
{
    /// <summary>
    /// Interaction logic for NewAnnouncement.xaml
    /// </summary>
    public partial class NewAnnouncement : Window
    {
        public NewAnnouncement()
        {
            InitializeComponent();
            Visibility.Items.Add("Svi");
            Visibility.Items.Add("Pacijenti");
            Visibility.Items.Add("Zaposleni");
            Visibility.Items.Add("Lekari");
            Visibility.Items.Add("Upravnik");
            Visibility.Items.Add("Sekretari");
            Visibility.SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementFileRepository ast = new AnnouncementFileRepository();
            int id = ast.generateNextId();
            DateTime po = DateTime.Today;
            DateTime ed = DateTime.Today;
            String con = ContentTextBox.Text;
            String tit = Title.Text;
            Model.Visibility vis = Model.Visibility.all;
            if(Visibility.SelectedIndex == 1)
                vis = Model.Visibility.patients;
            else if (Visibility.SelectedIndex == 2)
                vis = Model.Visibility.staff;
            else if (Visibility.SelectedIndex == 3)
                vis = Model.Visibility.doctors;
            else if (Visibility.SelectedIndex == 4)
                vis = Model.Visibility.menagers;
            else if (Visibility.SelectedIndex == 5)
                vis = Model.Visibility.secretaries;
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            AnnouncementFileRepository s = new AnnouncementFileRepository();
            s.Save(announcement);
            Announcements.Ans.Add(announcement);

            this.Close();

        }
    }
}
