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

namespace vezba.ManagerGUI
{
    public partial class ViewAnnouncementManagerPage : Page
    {
        public ViewAnnouncementManagerPage(Announcement a)
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

        }
    }
}
