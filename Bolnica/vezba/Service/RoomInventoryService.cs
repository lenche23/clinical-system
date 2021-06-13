using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;
using vezba.Service;

namespace Service
{
    public class RoomInventoryService
    {

        private IRoomInventoryRepository RoomInventoryRepository { get; }

        public RoomInventoryService()
        {
            RoomInventoryRepository = new RoomInventoryFileRepository();
        }

        // Lekar**********************************************************************************
        public Boolean TreatPatient(DateTime startDate, int numberOfDays, Room room)
        {
            var roomInventories = RoomInventoryRepository.GetAll();

            var endDate = startDate.AddDays(numberOfDays);

            var bedsInSelectedPeriod = new List<RoomInventory>();
            foreach (var inventory in roomInventories)
            {
                if (inventory.room.RoomNumber == room.RoomNumber && inventory.equipment.Name == "Krevet" && DateTime.Compare(startDate, inventory.EndTime) < 0 && DateTime.Compare(endDate, inventory.StartTime) > 0)
                {
                    bedsInSelectedPeriod.Add(inventory);
                }
            }
            if (bedsInSelectedPeriod.Count == 0)
                return false;
            foreach (var inventory in bedsInSelectedPeriod)
            {
                if (inventory.Quantity <= 0 || inventory.NumberUnavailable >= inventory.Quantity)
                    return false;
            }
            OccupyBed(startDate, endDate, room, bedsInSelectedPeriod);
            return true;
        }

        public void OccupyBed(DateTime startDate, DateTime endDate, Room room, List<RoomInventory> inventories)
        {
            foreach (var inventory in inventories)
            {
                if (DateTime.Compare(startDate, inventory.StartTime) > 0 && DateTime.Compare(endDate, inventory.EndTime) < 0)
                {
                    DeleteRoomInventory(inventory.Id);

                    var beforeTreatment = new RoomInventory(inventory.StartTime, startDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    beforeTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    SaveRoomInventory(beforeTreatment);

                    var duringTreatment = new RoomInventory(startDate, endDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    SaveRoomInventory(duringTreatment);

                    var afterTreatment = new RoomInventory(endDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    afterTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    SaveRoomInventory(afterTreatment);
                }
                else if (DateTime.Compare(startDate, inventory.StartTime) > 0)
                {
                    DeleteRoomInventory(inventory.Id);

                    var beforeTreatment = new RoomInventory(inventory.StartTime, startDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    beforeTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    SaveRoomInventory(beforeTreatment);

                    var duringTreatment = new RoomInventory(startDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    SaveRoomInventory(duringTreatment);
                }
                else if (DateTime.Compare(endDate, inventory.EndTime) < 0)
                {
                    DeleteRoomInventory(inventory.Id);

                    var duringTreatment = new RoomInventory(inventory.StartTime, endDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    SaveRoomInventory(duringTreatment);

                    var afterTreatment = new RoomInventory(endDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    afterTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    SaveRoomInventory(afterTreatment);
                }
                else
                {
                    inventory.NumberUnavailable++;
                    UpdateRoomInventory(inventory);
                }
            }
        }

        public void CancelPatientTreatment(DateTime startDate, int numberOfDays, Room room)
        {
            var roomInventories = RoomInventoryRepository.GetAll();

            var endDate = startDate.AddDays(numberOfDays);

            foreach (var inventory in roomInventories)
            {
                if (inventory.room.RoomNumber == room.RoomNumber && inventory.equipment.Name == "Krevet" && DateTime.Compare(startDate, inventory.StartTime) <= 0 && DateTime.Compare(endDate, inventory.EndTime) >= 0)
                {
                    inventory.NumberUnavailable--;
                    UpdateRoomInventory(inventory);
                }
            }
            MergeSameStatesInRoom(room, "Krevet");
        }

        public void MergeSameStatesInRoom(Room room, String equipmentName)
        {
            var roomInventories = RoomInventoryRepository.GetAll();
            var inventoriesForMerge = new List<RoomInventory>();
            foreach (var inventory in roomInventories)
            {
                if (inventory.room.RoomNumber == room.RoomNumber && inventory.equipment.Name == equipmentName)
                {
                    inventoriesForMerge.Add(inventory);
                }
            }

            var current = inventoriesForMerge[0];
            foreach (var inventory in inventoriesForMerge)
            {
                if (inventory.Id == current.Id)
                    continue;
                if(inventory.Quantity == current.Quantity && inventory.NumberUnavailable == current.NumberUnavailable)
                {
                    current.EndTime = inventory.EndTime;
                    DeleteRoomInventory(inventory.Id);
                    UpdateRoomInventory(current);
                }
                else
                {
                    current = inventory;
                }
            }
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************

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

        public int ChangeEquipmentQuantity(IStrategy strategy, RoomInventory roomInventory, int roomNumber, int inputItemQuantity, DateTime pickedDate)
        {
            return strategy.ChangeEquipmentQuantity(roomInventory, roomNumber, inputItemQuantity, pickedDate);
        }

        // UpravnikKraj***************************************************************************
    }
}