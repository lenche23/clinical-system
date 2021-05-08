using Bolnica;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public static ObservableCollection<Room> Rooms { get; set; }
        public static List<Room> rooms;
        private RoomStorage storage;

        public RoomsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new RoomStorage();
            rooms = storage.GetAll();
            Rooms = new ObservableCollection<Room>(rooms);
            lvDataBinding.ItemsSource = Rooms;
        }

        private void New_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new WindowNewRoom();
            s.Show();
        }

        private void Edit_Room_Button_Click(object sender, RoutedEventArgs e)
        {

            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItems[0];
                //var s = new WindowUpdateRoom(selected, this);
                //s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void Delete_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room r = (Room)lvDataBinding.SelectedItem;
                RoomStorage rs = new RoomStorage();
                rs.Delete(r.RoomNumber);
                Rooms.Remove(r);
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }


        }

        private void View_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItem;

                //var s = new WindowViewRoom(selected, this);
                //s.Show();
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void Renovations_Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedIndex > -1)
            {
                Room selected = (Room)lvDataBinding.SelectedItems[0];
                var s = new WindowRenovations(selected);
                s.Show();
            }

            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }
    }
}
