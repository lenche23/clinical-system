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

namespace vezba.ManagerGUI
{
    /// <summary>
    /// Interaction logic for TutorialSecondPage.xaml
    /// </summary>
    public partial class TutorialSecondPage : Page
    {
        private MainManagerWindow mainManagerWindow;
        public TutorialSecondPage(MainManagerWindow mainManagerWindow)
        {
            InitializeComponent();
            this.mainManagerWindow = mainManagerWindow;
            NextButton.IsEnabled = false;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Video.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            Video.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Video.Stop();
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new UserProfilePage(mainManagerWindow);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new TutorialPage(mainManagerWindow);
        }
    }
}
