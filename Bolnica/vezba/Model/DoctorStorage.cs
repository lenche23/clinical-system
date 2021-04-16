// File:    DoctorStorage.cs
// Author:  graho
// Created: 16 April 2021 17:52:57
// Purpose: Definition of Class DoctorStorage

using System;
using System.Collections.Generic;

namespace Model
{
   public class DoctorStorage
   {
      public List<Doctor> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean Save(Doctor d)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(Doctor d)
      {
         throw new NotImplementedException();
      }
      
      public Doctor GetOne(String jmbg)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Delete(String jmbg)
      {
         throw new NotImplementedException();
      }
      
      public List<Doctor> Load()
      {
         throw new NotImplementedException();
      }
      
      public String FileName { get; set; }

    }
}