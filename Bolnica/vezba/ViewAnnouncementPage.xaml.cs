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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace vezba
{
    /// <summary>
    /// Interaction logic for ViewAnnouncementPage.xaml
    /// </summary>
    public partial class ViewAnnouncementPage : Page
    {
        private DoctorView dw;

        public ViewAnnouncementPage(Announcement a, DoctorView dw)
        {
            InitializeComponent();
            Posted.Content += a.FormatedDatePosted;
            Edited.Content += a.FormatedDateEdited;
            if (Posted.Content.Equals(Edited.Content))
            {
                Edited.Visibility = System.Windows.Visibility.Collapsed;
            }
            Content.Text = a.Content;
            Title.Text = a.Title;
            this.dw = dw;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            dw.Main.GoBack();
        }
    }
}
