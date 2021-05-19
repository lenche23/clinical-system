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
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class RoomAddNewPage : Page
    {

        public RoomAddNewPage()
        {
            InitializeComponent();
        }

        private void Potvrdi_Button_Click(object sender, RoutedEventArgs e)
        {

            Floor floor = Floor.first;
            RoomType type = RoomType.examinationRoom;

            if (Convert.ToBoolean(Prvi.IsChecked))
            {
                floor = Floor.first;
            }

            else if (Convert.ToBoolean(Drugi.IsChecked))
            {
                floor = Floor.second;
            }

            else if (Convert.ToBoolean(Treci.IsChecked))
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

            var newRoom = new Room(int.Parse(BrojSobe.Text), floor, type) { };

            RoomFileRepository fileRepository = new RoomFileRepository();
            fileRepository.Save(newRoom);
            RoomsPage.Rooms.Add(newRoom);
            NavigationService.GoBack();
        }

        private void Odustani_Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GoBack();
        }
    }
}
