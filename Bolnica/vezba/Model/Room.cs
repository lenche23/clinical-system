using System;

namespace Model
{
    public class Room
    {
        public RoomType RoomType { get; set; }
        public int RoomNumber { get; set; }
        public Floor RoomFloor { get; set; }
        public Boolean IsDeleted { get; set; }

        public Room(int roomNumber, Floor roomFloor, RoomType roomType)
        {
            this.RoomType = roomType;
            this.RoomNumber = roomNumber;
            this.RoomFloor = roomFloor;
            this.Capacity = 0;
            this.IsDeleted = false;
        }

        public int Capacity;

        public System.Collections.Generic.List<Equipment> equipment;

        public System.Collections.Generic.List<Equipment> Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new System.Collections.Generic.List<Equipment>();
                return equipment;
            }
            set
            {
                RemoveAllEquipment();
                if (value != null)
                {
                    foreach (Equipment oEquipment in value)
                        AddEquipment(oEquipment);
                }
            }
        }

        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.Generic.List<Equipment>();
            if (!this.equipment.Contains(newEquipment))
                this.equipment.Add(newEquipment);
        }

        public void QuantityEquipment(Equipment changedEquipment, int quantity)
        {
            int id = changedEquipment.Id;
            if (changedEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.Generic.List<Equipment>();

            Boolean exist = false;

            for (int i = 0; i < this.equipment.Count; i++)
                {
                    if (this.equipment[i].Id == id)
                    {                      
                        this.equipment[i].Quantity = this.equipment[i].Quantity + quantity;
                        exist = true;
                    }
                }
          
            if (exist == false) 
            {
                if (quantity>0)
                {
                    Equipment e = new Equipment(changedEquipment.Id, changedEquipment.Name, quantity, changedEquipment.Type);
                       this.AddEquipment(e);
                }
            }
        }

        public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                    this.equipment.Remove(oldEquipment);
        }

        public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
        }

        public override string ToString()
        {
            return this.RoomNumber.ToString();
        }

    }
}