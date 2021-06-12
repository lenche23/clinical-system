using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
    public class RoomInventoryService
    {

        private IRoomInventoryRepository RoomInventoryRepository { get; }

        public RoomInventoryService()
        {
            RoomInventoryRepository = new RoomInventoryFileRepository();
        }

        public List<RoomInventory> GetAllRoomInventories(Room selected)
        {
            List<RoomInventory> roomInventoryList = new List<RoomInventory>();
            foreach (RoomInventory roomInventory in RoomInventoryRepository.GetAll())
            {
                if (roomInventory.room.RoomNumber == selected.RoomNumber)
                {
                    if (DateTime.Compare(roomInventory.StartTime, DateTime.Now) <= 0 &&
                        DateTime.Compare(roomInventory.EndTime, DateTime.Now) >= 0)
                    {
                        roomInventoryList.Add(roomInventory);
                    }
                }
            }

            return roomInventoryList;
        }

        public Boolean SaveRoomInventory(RoomInventory newRoomInventory)
        {
            return RoomInventoryRepository.Save(newRoomInventory);
        }

        public Boolean UpdateRoomInventory(RoomInventory updatedRoomInventory)
        {
            return RoomInventoryRepository.Update(updatedRoomInventory);
        }

        public Boolean DeleteRoomInventory(int roomInventoryId)
        {
            return RoomInventoryRepository.Delete(roomInventoryId);
        }

        public int NewDesiredRoomItemQuantity(RoomInventory roomInventory, int roomNumber, int inputItemQuantity, DateTime pickedDate)
        {
            var itemFound = false;
            var desiredRoomItemQuantity = 0;

            foreach (RoomInventory inventory in RoomInventoryRepository.GetAll())
            {
                if (inventory.room.RoomNumber == roomNumber && DateTime.Compare(inventory.StartTime, DateTime.Now) <= 0 && DateTime.Compare(inventory.EndTime, DateTime.Now) >= 0 && inventory.equipment.Id == roomInventory.equipment.Id)
                {
                    inventory.EndTime = pickedDate;
                    UpdateRoomInventory(inventory);
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

        public bool AddQuantityToDesiredRoom(int roomNumber, RoomInventory roomInventory, int itemQuantity)
        {
            var itemFound = false;
            foreach (RoomInventory inventory in RoomInventoryRepository.GetAll())
            {
                if (inventory.room.RoomNumber == roomNumber && inventory.equipment.Id == roomInventory.equipment.Id)
                {
                    inventory.Quantity += itemQuantity;
                    UpdateRoomInventory(inventory);
                    itemFound = true;
                }
            }
            return itemFound;
        }

        // UpravnikKraj***************************************************************************
    }
}