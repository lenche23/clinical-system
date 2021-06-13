using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezba.Repository;
using vezba.Service;

namespace vezba.Service
{
    class PatientStayRegulator
    {
        private IRoomInventoryRepository RoomInventoryRepository { get; }

        public PatientStayRegulator()
        {
            RoomInventoryRepository = new RoomInventoryFileRepository();
        }    

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
                    RoomInventoryRepository.Delete(inventory.Id);

                    var beforeTreatment = new RoomInventory(inventory.StartTime, startDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    beforeTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    RoomInventoryRepository.Save(beforeTreatment);

                    var duringTreatment = new RoomInventory(startDate, endDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    RoomInventoryRepository.Save(duringTreatment);

                    var afterTreatment = new RoomInventory(endDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    afterTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    RoomInventoryRepository.Save(afterTreatment);
                }
                else if (DateTime.Compare(startDate, inventory.StartTime) > 0)
                {
                    RoomInventoryRepository.Delete(inventory.Id);

                    var beforeTreatment = new RoomInventory(inventory.StartTime, startDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    beforeTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    RoomInventoryRepository.Save(beforeTreatment);

                    var duringTreatment = new RoomInventory(startDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    RoomInventoryRepository.Save(duringTreatment);
                }
                else if (DateTime.Compare(endDate, inventory.EndTime) < 0)
                {
                    RoomInventoryRepository.Delete(inventory.Id);

                    var duringTreatment = new RoomInventory(inventory.StartTime, endDate, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    duringTreatment.NumberUnavailable = inventory.NumberUnavailable + 1;
                    RoomInventoryRepository.Save(duringTreatment);

                    var afterTreatment = new RoomInventory(endDate, inventory.EndTime, inventory.Quantity, 0, inventory.equipment, inventory.room);
                    afterTreatment.NumberUnavailable = inventory.NumberUnavailable;
                    RoomInventoryRepository.Save(afterTreatment);
                }
                else
                {
                    inventory.NumberUnavailable++;
                    RoomInventoryRepository.Update(inventory);
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
                    RoomInventoryRepository.Update(inventory);
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
                if (inventory.Quantity == current.Quantity && inventory.NumberUnavailable == current.NumberUnavailable)
                {
                    current.EndTime = inventory.EndTime;
                    RoomInventoryRepository.Delete(inventory.Id);
                    RoomInventoryRepository.Update(current);
                }
                else
                {
                    current = inventory;
                }
            }
        }
    }
}
