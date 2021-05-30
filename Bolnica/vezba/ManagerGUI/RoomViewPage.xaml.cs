using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RoomViewPage : Page
{
    public static ObservableCollection<RoomInventory> RoomInventoryList { get; set; }
        public RoomViewPage(Room selected, RoomsPage mv)
        {
            InitializeComponent();
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
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            var roomInventoryList = roomInventoryService.RoomInventories(selected);
            RoomInventoryList = new ObservableCollection<RoomInventory>(roomInventoryList);
            RoomInventoryBinding.ItemsSource = RoomInventoryList;
        }
        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

