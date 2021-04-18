using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Model;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for WindowNewRoom.xaml
    /// </summary>
    public partial class WindowNewRoom : Window
    {

        public WindowNewRoom()
        {
            InitializeComponent();
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {
            
            Floor floor = Floor.first;
            RoomType type = RoomType.examinationRoom;

            if(Convert.ToBoolean(Prvi.IsChecked))
            {
                floor = Floor.first;
            }

            else if(Convert.ToBoolean(Drugi.IsChecked))
            {
                floor = Floor.second;
            }
            
            else if(Convert.ToBoolean(Treci.IsChecked))
            {
                floor = Floor.third;
            }


            if (Convert.ToBoolean(Pregled.IsChecked))
            {
                type = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija.IsChecked))
            {
                type = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor.IsChecked))
            {
                type = RoomType.recoveryRoom;
            }

            var newRoom = new Room() { RoomNumber = int.Parse(BrojSobe.Text), RoomFloor = floor, RoomType = type };

            RoomStorage storage = new RoomStorage();
            storage.Save(newRoom);
            ManagerView.Rooms.Add(newRoom);
            this.Close();
        }

        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
