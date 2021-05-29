using System.Windows.Controls;
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
