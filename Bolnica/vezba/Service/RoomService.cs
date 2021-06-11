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

        public List<Room> GetSearchResultRooms(String search)
        {
            List<Room> allRooms = GetAllRooms();
            List<Room> rooms = new List<Room>();
            Boolean flag = false;
            foreach (Room r in allRooms)
            {
                flag = false;
                if (r.RoomNumber.ToString().ToLower().Contains(search.ToLower()))
                    flag = true;
                if (r.RoomFloorName.ToLower().Contains(search.ToLower()))
                    flag = true;
                if (r.RoomTypeName.ToLower().Contains(search.ToLower()))
                    flag = true;
                if (flag == true)
                    rooms.Add(r);
            }
            return rooms;
        }
    }
}
