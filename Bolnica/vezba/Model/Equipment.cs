// File:    Equipment.cs
// Author:  graho
// Created: 16 April 2021 17:26:49
// Purpose: Definition of Class Equipment

using System;

namespace Model
{
   public class Equipment
   {

        public Equipment(int id, String name, EquipmentType equipmentType)
        {
            this.Id = id;
            this.Name = name;
            this.Type = equipmentType;
            this.IsDeleted = false;
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public EquipmentType Type { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}