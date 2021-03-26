using System;

namespace Model
{
    public class Room
    {
        public RoomType RoomType { get; set; }
        public int RoomNumber { get; set; }
        public Floor RoomFloor { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}