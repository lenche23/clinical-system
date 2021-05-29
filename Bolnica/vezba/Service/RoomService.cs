using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
    public class RoomService
    {
        public RoomFileRepository RoomRepository { get; }

        public RoomService()
        {
            RoomRepository = new RoomFileRepository();
        }


        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************

        public List<Room> GetAllRooms()
        {
            return RoomRepository.GetAll();
        }

        public Boolean SaveRoom(Room newRoom)
        {
            return RoomRepository.Save(newRoom);
        }

        public Boolean UpdateRoom(Room updatedRoom)
        {
            return RoomRepository.Update(updatedRoom);
        }

        public Boolean DeleteRoom(int roomId)
        {
            return RoomRepository.Delete(roomId);
        }

        public Room GetOneRoom(int roomId)
        {
            return RoomRepository.GetOne(roomId);
        }

        /*
        public int generateNextRoomId()
        {
            return RoomRepository.generateNextId();
        }
        */

        // UpravnikKraj***************************************************************************
    }
}
