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
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for WindowUpdateRoom.xaml
    /// </summary>
    public partial class WindowUpdateRoom : Window
    {
        private Room selected;
        private ManagerView mv;

        public WindowUpdateRoom(Room selected, ManagerView mv)
        {
            InitializeComponent();
            this.selected = selected;
            this.mv = mv;
            this.DataContext = selected;
            if(selected.RoomFloor == Floor.first)
            {
                Prvi.IsChecked = true;
            }
            else if(selected.RoomFloor == Floor.second)
            {
                Drugi.IsChecked = true;
            }
            else if(selected.RoomFloor == Floor.third)
            {
                Treci.IsChecked = true;
            }


            if(selected.RoomType == RoomType.examinationRoom)
            {
                Pregled.IsChecked = true;
            }
            else if(selected.RoomType == RoomType.operatingRoom)
            {
                Operacija.IsChecked = true;
            }
            else if(selected.RoomType == RoomType.recoveryRoom)
            {
                Odmor.IsChecked = true;
            }


        }

        private void Potvrda_Button_Click(object sender, RoutedEventArgs e)
        {
            var RoomNumber = int.Parse(BrojSobe.Text);
            selected.RoomNumber = RoomNumber;

            

            if (Convert.ToBoolean(Prvi.IsChecked))
            {
                selected.RoomFloor = Floor.first;
            }

            else if (Convert.ToBoolean(Drugi.IsChecked))
            {
                selected.RoomFloor = Floor.second;
            }

            else if (Convert.ToBoolean(Treci.IsChecked))
            {
                selected.RoomFloor = Floor.third;
            }


            if (Convert.ToBoolean(Pregled.IsChecked))
            {
                selected.RoomType = RoomType.examinationRoom;
            }

            else if (Convert.ToBoolean(Operacija.IsChecked))
            {
                selected.RoomType = RoomType.operatingRoom;
            }

            else if (Convert.ToBoolean(Odmor.IsChecked))
            {
                selected.RoomType = RoomType.recoveryRoom;
            }

            mv.lvDataBinding.Items.Refresh();
            this.Close();
        }

        private void Odustanak_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
