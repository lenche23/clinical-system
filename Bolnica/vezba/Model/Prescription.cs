// File:    Prescription.cs
// Author:  graho
// Created: 16 April 2021 17:26:56
// Purpose: Definition of Class Prescription

using System;

namespace Model
{
   public class Prescription
   {
        public Prescription(int Id, DateTime StartDate, int DurationInDays, Period ReferencePeriod, int Number, Medicine Medicine)
        {
            this.Id = Id;
            this.StartDate = StartDate;
            this.DurationInDays = DurationInDays;
            this.ReferencePeriod = ReferencePeriod;
            this.Number = Number;
            this.Medicine = Medicine;
        }

      public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
        public Period ReferencePeriod { get; set; }
        public int Number { get; set; }
        public int Id { get; set; }
        public Boolean isActive { get; set; }

        public Medicine Medicine { get; set; }

    }
}