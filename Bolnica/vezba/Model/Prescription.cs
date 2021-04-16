// File:    Prescription.cs
// Author:  graho
// Created: 16 April 2021 17:26:56
// Purpose: Definition of Class Prescription

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

    }
}