using Model;
using System;
using System.Collections.Generic;
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
    }
}
