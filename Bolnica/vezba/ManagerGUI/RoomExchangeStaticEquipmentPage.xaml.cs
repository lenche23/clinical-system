using Bolnica;
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
using System.Timers;
using System.Threading;
using vezba.Repository;

namespace vezba.ManagerGUI
{
    public partial class RoomExchangeStaticEquipmentPage : Page
    {
        private RoomInventory roomInventory;
        private Room room;
        private DateTime pickedDate;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private int inputItemQuantity;
        private int currentRoomItemQuantity;
        private int desiredRoomItemQuantity;
        private RoomInventoryFileRepository _roomInventoryFileRepository;

        public RoomExchangeStaticEquipmentPage(RoomInventory roomInventory, Room room)
        {
            InitializeComponent();
            this.roomInventory = roomInventory;
            this.room = room;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int roomNumber = int.Parse(BrojSobe.Text);
            inputItemQuantity = int.Parse(Količina.Text);
            currentRoomItemQuantity = roomInventory.Quantity;
            //pickedDate = Date.SelectedDate.Value.Date;
            pickedDate = DateTime.Now.AddSeconds(5);

            RoomFileRepository roomFileRepository = new RoomFileRepository();
            Room roomEntry = roomFileRepository.GetOne(roomNumber);
            _roomInventoryFileRepository = new RoomInventoryFileRepository();

            if (Validate(roomEntry) == false)
                return;

            roomInventory.EndTime = pickedDate;
            _roomInventoryFileRepository.Update(this.roomInventory);

            currentRoomItemQuantity = roomInventory.Quantity - inputItemQuantity;
            desiredRoomItemQuantity = NewDesiredRoomItemQuantity(roomNumber);

            SaveNewRoomInventory(currentRoomItemQuantity, room);
            SaveNewRoomInventory(desiredRoomItemQuantity, roomEntry);

            NavigationService.GoBack();
            //this.Close();
        }

        private void SaveNewRoomInventory(int newItemQuantity, Room roomEntry)
        {
            var id = _roomInventoryFileRepository.GenerateNextId();
            RoomInventory ri = new RoomInventory(pickedDate, infiniteTime, newItemQuantity, id, roomInventory.equipment, roomEntry);
            _roomInventoryFileRepository.Save(ri);
        }

        private int NewDesiredRoomItemQuantity(int roomNumber)
        {
            var itemFound = false;

            foreach (RoomInventory inventory in _roomInventoryFileRepository.GetAll())
            {
                if (inventory.room.RoomNumber == roomNumber && DateTime.Compare(inventory.StartTime, DateTime.Now) <= 0 && DateTime.Compare(inventory.EndTime, DateTime.Now) >= 0 && inventory.equipment.Id == roomInventory.equipment.Id)
                {
                    inventory.EndTime = pickedDate;
                    _roomInventoryFileRepository.Update(inventory);
                    desiredRoomItemQuantity = inventory.Quantity + inputItemQuantity;
                    itemFound = true;
                }
            }

            if (!itemFound)
            {
                desiredRoomItemQuantity = inputItemQuantity;
            }

            return desiredRoomItemQuantity;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            //this.Close();
        }

        public Boolean Validate(Room roomEntry)
        {
            if (roomEntry == null)
            {
                MessageBox.Show("Soba ne postoji");
                return false;
            }

            if (roomEntry.RoomNumber == this.room.RoomNumber)
            {
                MessageBox.Show("Soba ne može biti trenutno selektovana soba");
                return false;
            }

            if (currentRoomItemQuantity < inputItemQuantity)
            {
                MessageBox.Show("Uneta količina robe prekoračava maksimalnu postojeću u sobi");
                return false;
            }

            if (DateTime.Compare(pickedDate, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je već prošao!");
                return false;
            }

            return true;
        }
    }
}

/*
namespace vezba
{
    /// <summary>
    /// Interaction logic for WindowExchangeStaticEquipment.xaml
    /// </summary>
    public partial class WindowExchangeStaticEquipment : Window
    {
        public WindowUpdateRoom wur;
        public Room room;
        public RoomInventory roomInventory;
        public Equipment eq;
        public ManagerView mv;

        public WindowExchangeStaticEquipment(RoomInventory selected, WindowUpdateRoom wur, Room r_selected, ManagerView mv)
        {
            InitializeComponent();
            this.wur = wur;
            this.roomInventory = selected;
            this.room = r_selected;
            this.mv = mv;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            int id_sobe = int.Parse(BrojSobe.Text);
            int kolicina_robe = int.Parse(Količina.Text);
            int maks_kolicina = this.roomInventory.Quantity;

            if (DateTime.Compare(Date.SelectedDate.Value.Date, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je vec prosao!");
            }
            else
            {
                //DateTime datum = Date.SelectedDate.Value.Date;
                int newQuantity1 = this.roomInventory.Quantity - kolicina_robe;
                int newQuantity2 = 0;

                RoomFileRepository rs = new RoomFileRepository();
                RoomInventoryFileRepository ris = new RoomInventoryFileRepository();
                Room room1 = rs.GetOne(id_sobe);
                DateTime time = DateTime.Now.AddSeconds(10);
                var exists = false;

                if (room1 != null && room1 != this.room)
                {
                    if (maks_kolicina >= kolicina_robe)
                    {
                        this.roomInventory.EndTime = time;
                        ris.UpdateMedicine(this.roomInventory);

                        foreach (RoomInventory ri in ris.GetDeclined())
                        {
                            if (ri.room.RoomNumber == id_sobe && DateTime.Compare(ri.StartTime, DateTime.Now) <= 0 && DateTime.Compare(ri.EndTime, DateTime.Now) >= 0 && ri.equipment.Id == roomInventory.equipment.Id) 
                            {
                                 ri.EndTime = time;
                                 newQuantity2 = ri.Quantity + kolicina_robe;
                                 ris.UpdateMedicine(ri);
                                 exists = true;                   
                            }
                        }
         
                        if (exists == false)
                        {
                            newQuantity2 = kolicina_robe;
                        }
                    }

                    var endTime = new DateTime(2999, 12, 31);
                    var id1 = ris.GenerateNextId();
                    RoomInventory ri1 = new RoomInventory(time, endTime, newQuantity1, id1, roomInventory.equipment, room);
                    ris.SaveDeclinedMedicine(ri1);
                    var id2 = ris.GenerateNextId();
                    RoomInventory ri2 = new RoomInventory(time, endTime, newQuantity2, id2, roomInventory.equipment, room1);
                    ris.SaveDeclinedMedicine(ri2);
                    this.Close();
                }
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
*/