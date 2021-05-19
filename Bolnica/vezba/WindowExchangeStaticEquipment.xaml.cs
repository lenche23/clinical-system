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

namespace vezba
{
    public partial class WindowExchangeStaticEquipment : Window
    {
        private RoomInventory roomInventory;
        private Room room;
        private DateTime pickedDate;
        private DateTime infiniteTime = new DateTime(2999, 12, 31);
        private int inputItemQuantity;
        private int currentRoomItemQuantity;
        private int desiredRoomItemQuantity;
        private RoomInventoryFileRepository _roomInventoryFileRepository;

        public WindowExchangeStaticEquipment(RoomInventory roomInventory, Room room)
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

            if (Validate(roomEntry) == false)
                return;

            _roomInventoryFileRepository = new RoomInventoryFileRepository();

            roomInventory.EndTime = pickedDate;
            _roomInventoryFileRepository.Update(this.roomInventory);

            currentRoomItemQuantity = roomInventory.Quantity - inputItemQuantity;
            desiredRoomItemQuantity = NewDesiredRoomItemQuantity(roomNumber);

            SaveNewRoomInventory(currentRoomItemQuantity, room);
            SaveNewRoomInventory(desiredRoomItemQuantity, roomEntry);

            this.Close();
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
            this.Close();
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