using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
   public class MedicineStorage
   {

        public MedicineStorage()
        {
            this.FileName = "../../lekovi.json";
        }

        public List<Medicine> GetAll()
      {
            List<Medicine> ms = new List<Medicine>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Medicine> medicineList = JsonConvert.DeserializeObject<List<Medicine>>(jsonFromFile);

                for (int i = 0; i < medicineList.Count; i++)
                {
                    if (medicineList[i].IsDeleted == false)
                    {
                        ms.Add(medicineList[i]);
                    }
                }
                return ms;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return ms;
        }
      
      public Boolean Save(Medicine medicine)
      {
            List<Medicine> medicineList = GetAll();
            medicineList.Add(medicine);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(medicineList, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e)
            {

            }

            return true;
        }
      
      public Boolean Update(Medicine medicine)
      {
            List<Medicine> medicineList = GetAll();
            for (int i = 0; i < medicineList.Count; i++)
            {
                if (medicineList[i].MedicineID.Equals(medicine.MedicineID))
                {
                    medicineList[i].Manufacturer = medicine.Manufacturer;
                    medicineList[i].Name = medicine.Name;
                    medicineList[i].Packaging = medicine.Packaging;
                    medicineList[i].Status = medicine.Status;
                    medicineList[i].ingridient = medicine.ingridient;
                    medicineList[i].Condition = medicine.Condition;
                    medicineList[i].ReplacementMedicine = medicine.ReplacementMedicine;
                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(medicineList, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }

                    catch (Exception e)
                    {

                    }
                }
            }
            return false;
        }
      
      public Medicine GetOne(int id)
      {
            List<Medicine> medicineList = GetAll();
            for (int i = 0; i < medicineList.Count; i++)
            {
                if (medicineList[i].MedicineID.Equals(id))
                {
                    return medicineList[i];
                }
            }
            return null;
        }
      
      public Boolean Delete(int id)
      {
            List<Medicine> medicineList = GetAll();
            for (int i = 0; i < medicineList.Count; i++)
            {
                if (medicineList[i].MedicineID.Equals(id))
                {
                    medicineList[i].IsDeleted = true;


                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(medicineList, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }

                    catch (Exception e)
                    {

                    }
                }
            }
            return false;
        }
      
      public List<Medicine> Load()
      {
            List<Medicine> m = new List<Medicine>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Medicine> medicineList = JsonConvert.DeserializeObject<List<Medicine>>(jsonFromFile);
                return medicineList;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return m;
        }

        public int GenerateNextId()
        {
            List<Medicine> list = Load();
            return list.Count;
        }

      public List<Medicine> GetAwaiting()
      {
            List<Medicine> ms = new List<Medicine>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Medicine> medicineList = JsonConvert.DeserializeObject<List<Medicine>>(jsonFromFile);

                for (int i = 0; i < medicineList.Count; i++)
                {
                    if (medicineList[i].IsDeleted == false && medicineList[i].Status == MedicineStatus.awaiting)
                    {
                        ms.Add(medicineList[i]);
                    }
                }
                return ms;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return ms;
        }

        public List<Medicine> GetApproved()
        {
            List<Medicine> ms = new List<Medicine>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Medicine> medicineList = JsonConvert.DeserializeObject<List<Medicine>>(jsonFromFile);

                for (int i = 0; i < medicineList.Count; i++)
                {
                    if (medicineList[i].IsDeleted == false && medicineList[i].Status == MedicineStatus.approved)
                    {
                        ms.Add(medicineList[i]);
                    }
                }
                return ms;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return ms;
        }

        public String FileName { get; set; }
    }
}