using System.ComponentModel;
using Model;
using System.Windows;
using System.Windows.Controls;
using Service;
using System;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class RoomChangeEquipmentPage : Page, INotifyPropertyChanged
    {
        private RoomInventory selected;
        private RoomUpdatePage wur;
        private MainManagerWindow mainManagerWindow;
        private int kolicinaRobe;

        public event PropertyChangedEventHandler PropertyChanged;

        public int KolicinaRobe
        {
            get
            {
                return kolicinaRobe;
            }
            set
            {
                if (value != kolicinaRobe)
                {
                    kolicinaRobe = value;
                    OnPropertyChanged("KolicinaRobe");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public RoomChangeEquipmentPage(MainManagerWindow mainManagerWindow, RoomInventory selected, RoomUpdatePage wur)
        {
            InitializeComponent();
            DataContext = this;
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

            KolicinaRobe = selected.Quantity;
        }

        private void PotvrdiIzmenu_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateEntries()) return;
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

        private void Količina_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Količina.Text == "")
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }


        private Boolean ValidateEntries()
        {
            Količina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(Količina))
            {
                return false;
            }
            return true;
        }
    }
}
