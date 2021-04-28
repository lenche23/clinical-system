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
    /// Interaction logic for NewIndividualAnouncement.xaml
    /// </summary>
    public partial class NewIndividualAnouncement : Window
    {
        public NewIndividualAnouncement()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientStorage ps = new PatientStorage();
            List<Patient> patients = ps.GetAll();
            Patient.ItemsSource = patients;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementStorage ast = new AnnouncementStorage();
            int id = ast.generateNextId();
            DateTime po = DateTime.Today;
            DateTime ed = DateTime.Today;
            String con = Content.Text;
            String tit = Title.Text;
            Model.Visibility vis = Model.Visibility.individual;
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Niste uneli primaoca!");
                return;
            }
            Patient patient = (Patient)Patient.SelectedItem;
            Announcement announcement = new Announcement(id, po, ed, tit, con, vis);
            announcement.AddRecipient(patient.Jmbg);
            AnnouncementStorage s = new AnnouncementStorage();
            s.Save(announcement);
            Announcements.Ans.Add(announcement);

            this.Close();
        }
    }
}
