using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Template
{
    class SearchRooms : Search<Room>
    {
        protected override List<Room> GetAll()
        {
            RoomService roomService = new RoomService();
            List<Room> allRooms = roomService.GetAllRooms();
            return allRooms;
        }

        protected override bool ItemContainsInput(Room room, string input)
        {
            Boolean flag = false;
            if (room.RoomNumber.ToString().ToLower().Contains(input.ToLower()))
                flag = true;
            if (room.RoomFloorName.ToLower().Contains(input.ToLower()))
                flag = true;
            if (room.RoomTypeName.ToLower().Contains(input.ToLower()))
                flag = true;
            return flag;
        }
    }
}
