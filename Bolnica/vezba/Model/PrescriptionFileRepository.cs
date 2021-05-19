// File:    PrescriptionFileRepository.cs
// Author:  graho
// Created: 16 April 2021 17:13:01
// Purpose: Definition of Class PrescriptionFileRepository

using System;
using System.Collections.Generic;

namespace Model
{
   public class PrescriptionFileRepository
   {
      public List<Prescription> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean Save(Prescription p)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(Prescription p)
      {
         throw new NotImplementedException();
      }
      
      public Prescription GetOne(int id)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Prescription> Load()
      {
         throw new NotImplementedException();
      }
      
      public String FileName { get; set; }

    }
}