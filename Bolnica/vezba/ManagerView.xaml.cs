using System.Windows;
using Model;
using System.Collections.Generic;
using Bolnica;
using System.Collections.ObjectModel;
using System.Windows;

namespace vezba
{
    public partial class ManagerView : Window
    {
        public static ObservableCollection<Room> Rooms { get; set; }

        private RoomStorage storage;

        public ManagerView()
        {
            InitializeComponent();
            this.DataContext = this;
            storage = new RoomStorage();
            List<Room> rooms = storage.GetAll();
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

            if(lvDataBinding.SelectedIndex>-1)
            {
                Room selected = (Room)lvDataBinding.SelectedItems[0];
                var s = new WindowUpdateRoom(selected,this);
                s.Show();
            }
            
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }

        private void Delete_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if(lvDataBinding.SelectedIndex>-1)
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

                var s = new WindowViewRoom(selected, this);
                s.Show();
            }
            else
            {
                MessageBox.Show("Ni jedna prostorija nije selektovana!");
            }
        }
    }
}
