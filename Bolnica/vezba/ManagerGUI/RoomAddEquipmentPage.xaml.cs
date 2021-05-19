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


namespace vezba.ManagerGUI
{
    public partial class RoomAddEquipmentPage : Page
    {
        private Room selected;
        public RoomAddEquipmentPage(Room selected)
        {
            InitializeComponent();
            this.selected = selected;
            Title.Content = Title.Content + " " + selected.RoomNumber;
            EquipmentStorage es = new EquipmentStorage();
            List<Equipment> equipmentList = es.GetAll();
            comboEquipmentName.ItemsSource = equipmentList;
        }

        private void PotvrdiDodavanje_Button_Click(object sender, RoutedEventArgs e)
        {
            Equipment comboEquipment = (Equipment)comboEquipmentName.SelectedItem;
            var Quantity = int.Parse(Količina.Text);
            var endTime = new DateTime(2999, 1, 1, 0, 0, 0);
            RoomInventoryStorage ris = new RoomInventoryStorage();
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
