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
using System.Windows.Shapes;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for WindowViewRoom.xaml
    /// </summary>
    public partial class WindowViewRoom : Window
    {
        private Room selected;
        private ManagerView mv;
        public static ObservableCollection<Equipment> EquipmentList { get; set; }
        public WindowViewRoom(Room selected, ManagerView mv)
        {
            InitializeComponent();
            this.selected = selected;
            this.mv = mv;

            BrProstorijeLabel.Content = BrProstorijeLabel.Content + " " + selected.RoomNumber;

            if (selected.RoomFloor == Floor.first)
            {
                SpratLabel.Content = SpratLabel.Content + " Prvi";
            }
            else if (selected.RoomFloor == Floor.second)
            {
                SpratLabel.Content = SpratLabel.Content + " Drugi";
            }
            else if (selected.RoomFloor == Floor.third)
            {
                SpratLabel.Content = SpratLabel.Content + " Treci";
            }


            if (selected.RoomType == RoomType.examinationRoom)
            {
                TipLabel.Content = TipLabel.Content + " Prostorija za pregled";
            }
            else if (selected.RoomType == RoomType.operatingRoom)
            {
                TipLabel.Content = TipLabel.Content + " Operaciona sala";
            }
            else if (selected.RoomType == RoomType.recoveryRoom)
            {
                TipLabel.Content = TipLabel.Content + " Prostorija za odmor";
            }

            List<Equipment> equipmentList = selected.equipment;
            EquipmentList = new ObservableCollection<Equipment>(equipmentList);
            EquipmentBinding.ItemsSource = EquipmentList;
        }
    }
}
