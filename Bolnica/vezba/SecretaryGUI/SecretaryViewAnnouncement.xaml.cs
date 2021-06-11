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

namespace vezba.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for SecretaryViewAnnouncement.xaml
    /// </summary>
    public partial class SecretaryViewAnnouncement : Window
    {
        public SecretaryViewAnnouncement(Announcement a)
        {
            InitializeComponent();
            PostedDate.Content = a.FormatedDatePosted;
            EditedDate.Content = "(izmenjeno " + a.FormatedDateEdited + ")";
            if (a.FormatedDatePosted.Equals(a.FormatedDateEdited))
            {
                EditedDate.Visibility = System.Windows.Visibility.Collapsed;
            }
            Title.Text = a.Title;
            Content.Text = a.Content;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
            else if (e.Key == Key.Enter)
                this.Close();
        }
    }
}

