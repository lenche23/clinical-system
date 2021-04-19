// File:    PrescriptionStorage.cs
// Author:  graho
// Created: 16 April 2021 17:13:01
// Purpose: Definition of Class PrescriptionStorage

using System;
using System.Collections.Generic;

namespace Model
{
   public class PrescriptionStorage
   {
      public List<Prescription> GetAll()
      {
            return Load();
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
            List<Prescription> prescriptions = new List<Prescription>();
            Medicine medicine = new Medicine();
            medicine.Name = "Vitamin C";
            Prescription p1 = new Prescription(DateTime.Now, 21, Period.daily, 1,0, true, medicine);
            prescriptions.Add(p1);
            Medicine medicine2 = new Medicine();
            medicine2.Name = "Brufen";
            Prescription p2 = new Prescription(DateTime.Now, 21, Period.daily, 2, 1, true, medicine2);
            prescriptions.Add(p2);

            return prescriptions;
      }
      
      public String FileName { get; set; }

    }
}