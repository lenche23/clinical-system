// File:    RoomInventoryStorage.cs
// Author:  graho
// Created: 26 April 2021 15:31:43
// Purpose: Definition of Class RoomInventoryStorage

using System;
using System.Collections.Generic;

namespace Model
{
   public class RoomInventoryStorage
   {
      public List<RoomInventory> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean Save(RoomInventory inventory)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(RoomInventory inventory)
      {
         throw new NotImplementedException();
      }
      
      public RoomInventory GetOne(int id)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<RoomInventory> Load()
      {
         throw new NotImplementedException();
      }
      
      public int GenerateNextId()
      {
         throw new NotImplementedException();
      }
      
      public String FileName { get; set; }

    }
}