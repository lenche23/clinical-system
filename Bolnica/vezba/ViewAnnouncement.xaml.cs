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
    
    public partial class ViewAnnouncement : Window
    {
        public ViewAnnouncement(Announcement a)
        {
            InitializeComponent();
            Posted.Content = a.Posted.ToString("dd.MM.yyyy.");
            Edited.Content = a.Edited.ToString("dd.MM.yyyy.");
            if (Posted.Content.Equals(Edited.Content)) {
                Edited.Visibility = System.Windows.Visibility.Collapsed;
            }
            Content.Text = a.Content;
            Title.Text = a.Title;

        }
    }
}