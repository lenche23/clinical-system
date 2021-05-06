using System;

namespace Model
{
   public class RoomInventory
   {

        public RoomInventory(DateTime startTime, DateTime endTime, int quantity, int id, Equipment equipment, Room room)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.equipment = equipment;
            this.room = room;
            this.Quantity = quantity;
            this.IsDeleted = false;
            this.Id = id;

        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
        public Boolean IsDeleted { get; set; }

        public Equipment equipment;

        public Room room;

        public String EquipmentName
        {
            get
            {
                if (equipment != null)
                    return (equipment.Name);
                else
                    return "";
            }
        }
        public String EquipmentId
        {
            get
            {
                if (equipment != null)
                    return ("" + equipment.Id);
                else
                    return "";
            }
        }

        public String EquipmentType
        {
            get
            {
                if (equipment != null)
                    return ("" + equipment.Type);
                else
                    return "";
            }
        }
    }
}