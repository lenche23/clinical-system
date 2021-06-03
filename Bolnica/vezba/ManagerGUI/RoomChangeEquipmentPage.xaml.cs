using Model;
using System.Windows;
using System.Windows.Controls;
using Service;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class RoomChangeEquipmentPage : Page
    {
        private RoomInventory selected;
        private RoomUpdatePage wur;
        private MainManagerWindow mainManagerWindow;
        public RoomChangeEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory selected, RoomUpdatePage wur)
        {
            InitializeComponent();
            this.DataContext = selected;
            this.mainManagerWindow = mainManagerWindow;
            this.selected = selected;
            this.wur = wur;
            NazivOpreme.Content = NazivOpreme.Content + "    " + selected.equipment.Name;
            Id.Content = Id.Content + "    " + selected.equipment.Id;

            if (selected.equipment.Type == EquipmentType.dinamical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Dinamička";
            }
            else if (selected.equipment.Type == EquipmentType.statical)
            {
                TipOpreme.Content = TipOpreme.Content + "    Statička";
            }
        }

        private void PotvrdiIzmenu_Button_Click(object sender, RoutedEventArgs e)
        {
            selected.Quantity = int.Parse(Količina.Text);
            RoomInventoryService roomInventoryService = new RoomInventoryService();
            roomInventoryService.UpdateRoomInventory(this.selected);
            wur.RoomInventoryBinding.Items.Refresh();
            NavigationService.GoBack();
        }
        private void OdustaniIzmena_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }
    }
}
