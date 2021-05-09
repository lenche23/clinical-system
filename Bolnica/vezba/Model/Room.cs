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
            this.IsDeleted = false;
            this.renovation = new System.Collections.Generic.List<Renovation>();

        }

        
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

        public override string ToString()
        {
            return this.RoomNumber.ToString();
        }

    }
}