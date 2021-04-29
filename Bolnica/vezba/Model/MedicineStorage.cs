using System;
using System.Collections.Generic;

namespace Model
{
   public class MedicineStorage
   {
      public List<Medicine> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean Save(Medicine medicine)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(Medicine medicine)
      {
            List<Medicine> medicineList = Load();
            for(int i=0; i<medicineList.Count; i++)
            {
                if(medicineList[i].MedicineID == medicine.MedicineID)
                {
                    medicineList[i].Name = medicine.Name;
                    medicineList[i].Manufacturer = medicine.Manufacturer;
                    medicineList[i].Packaging = medicine.Packaging;
                    medicineList[i].Status = medicine.Status;
                    medicineList[i].Condition = medicine.Condition;
                    medicineList[i].ReplacementMedicine = medicine.ReplacementMedicine;
                    medicineList[i].Ingridient = medicine.Ingridient;
                    return true;
                }
            }
            return false;
        }
      
      public Medicine GetOne(int id)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Medicine> Load()
      {
            List<Medicine> medicine = new List<Medicine>();
            Medicine m1 = new Medicine("Ibuprofen", "Union-medic d.o.o. Novi Sad", "30x400mg", 0, MedicineStatus.awaiting, MedicineCondition.pill);
            medicine.Add(m1);
            Ingridient i1 = new Ingridient("Neki alergen");
            Medicine m2 = new Medicine("Panklav", "Union-medic d.o.o. Novi Sad", "30x400mg", 0, MedicineStatus.approved, MedicineCondition.pill);
            m2.AddIngridient(i1);
            medicine.Add(m2);
            return medicine;
      }

      public List<Medicine> GetAwaiting()
      {
            List<Medicine> medicine = new List<Medicine>();
            List<Medicine> medicine1 = Load();
            for (int i = 0; i < medicine1.Count; i++) 
            {
                if(medicine1[i].Status.Equals(MedicineStatus.awaiting))
                {
                    medicine.Add(medicine1[i]);
                }
            }
            return medicine;
      }

        public List<Medicine> GetApproved()
        {
            List<Medicine> medicine = new List<Medicine>();
            List<Medicine> medicine1 = Load();
            for (int i = 0; i < medicine1.Count; i++)
            {
                if (medicine1[i].Status.Equals(MedicineStatus.approved))
                {
                    medicine.Add(medicine1[i]);
                }
            }
            return medicine;
        }

        public String FileName { get; set; }
    }
}