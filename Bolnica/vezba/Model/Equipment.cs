// File:    Equipment.cs
// Author:  graho
// Created: 16 April 2021 17:26:49
// Purpose: Definition of Class Equipment

using System;

namespace Model
{
   public class Equipment
   {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }
        public EquipmentType Type { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}