using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
    public class RoomInventoryService
    {

        public RoomInventoryFileRepository RoomInventoryRepository { get; }

        public RoomInventoryService()
        {
            RoomInventoryRepository = new RoomInventoryFileRepository();
        }


        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************

        public List<RoomInventory> GetAllRoomInventories()
        {
            return RoomInventoryRepository.GetAll();
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

        public int GenerateNextRoomInventoryId()
        {
            return RoomInventoryRepository.GenerateNextId();
        }

        // UpravnikKraj***************************************************************************
    }
}