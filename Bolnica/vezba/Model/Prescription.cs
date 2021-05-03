using System;

namespace Model
{
   public class Prescription
   {
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
        public Period ReferencePeriod { get; set; }
        public int Number { get; set; }
        public int Id { get; set; }
        public Boolean isActive { get; set; }

        public Medicine Medicine { get; set; }

        public Prescription(DateTime time, int d, Period rp, int n, int id, Boolean a, Medicine m)
        {
            this.StartDate = time;
            this.DurationInDays = d;
            this.ReferencePeriod = rp;
            this.Number = n;
            this.Id = id;
            this.isActive = a;
            this.Medicine = m;
        }
    }
}