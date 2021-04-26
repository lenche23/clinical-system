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
            this.Capacity = 0; //********
            this.IsDeleted = false;
        }

        //******************* IZBACITI CAPACITY ODAVDE - ON JE U ROOM INVENTORY
        public int Capacity;
        //*******************

        //######### SVE ODAVDE DO SLEDECIH TARABA TREBA IZBACITI
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

        //####################

         /*
         // OVO JE DEO KOJI TREBA DA OSTANE

         public System.Collections.Generic.List<Renovation> renovation;
      
          public System.Collections.Generic.List<Renovation> Renovation
          {
             get
             {
                if (renovation == null)
                   renovation = new System.Collections.Generic.List<Renovation>();
                return renovation;
             }
             set
             {
                RemoveAllRenovation();
                if (value != null)
                {
                   foreach (Renovation oRenovation in value)
                      AddRenovation(oRenovation);
                }
             }
          }
      
          public void AddRenovation(Renovation newRenovation)
          {
             if (newRenovation == null)
                return;
             if (this.renovation == null)
                this.renovation = new System.Collections.Generic.List<Renovation>();
             if (!this.renovation.Contains(newRenovation))
                this.renovation.Add(newRenovation);
          }
      
          public void RemoveRenovation(Renovation oldRenovation)
          {
             if (oldRenovation == null)
                return;
             if (this.renovation != null)
                if (this.renovation.Contains(oldRenovation))
                   this.renovation.Remove(oldRenovation);
          }
     
          public void RemoveAllRenovation()
          {
             if (renovation != null)
                renovation.Clear();
          }
         
         
         */

        public override string ToString()
        {
            return this.RoomNumber.ToString();
        }

    }
}