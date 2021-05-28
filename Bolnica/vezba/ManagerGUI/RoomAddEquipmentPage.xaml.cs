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
using System.Windows.Shapes;
using vezba.Repository;


namespace vezba.ManagerGUI
{
    public partial class RoomAddEquipmentPage : Page
    {
        private Room selected;
        private MainManagerWindow mainManagerWindow;
        public RoomAddEquipmentPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            this.mainManagerWindow = mainManagerWindow;
            Title.Content = Title.Content + " " + selected.RoomNumber;
            EquipmentFileRepository es = new EquipmentFileRepository();
            List<Equipment> equipmentList = es.GetAll();
            comboEquipmentName.ItemsSource = equipmentList;
        }

        private void PotvrdiDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            Equipment comboEquipment = (Equipment)comboEquipmentName.SelectedItem;
            var Quantity = int.Parse(Količina.Text);
            var endTime = new DateTime(2999, 1, 1, 0, 0, 0);
            RoomInventoryFileRepository ris = new RoomInventoryFileRepository();
            var id = ris.GenerateNextId();
            RoomInventory newRoomInventory = new RoomInventory(DateTime.Now, endTime, Quantity, id, comboEquipment, this.selected);
            ris.Save(newRoomInventory);
            RoomUpdatePage.RoomInventoryList.Add(newRoomInventory);
            NavigationService.GoBack();
        }

        private void OdustaniDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
