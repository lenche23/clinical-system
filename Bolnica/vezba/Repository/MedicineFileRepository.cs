using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
    public class MedicineFileRepository
    {
        public String FileName { get; set; }

        public MedicineFileRepository()
        {
            FileName = "../../lekovi.json";
        }

        public List<Medicine> GetAll()
        {
            var allMedicine = new List<Medicine>();
            var medicineFromFile = ReadFromFile();
            foreach (var medicine in medicineFromFile)
            {
                if (medicine.IsDeleted == false)
                {
                    allMedicine.Add(medicine);
                }
            }
            return allMedicine;
        }
        public Boolean Save(Medicine newMedicine)
        {
            var storedMedicine = ReadFromFile();
            foreach (var medicine in storedMedicine)
            {
                if (medicine.MedicineID == newMedicine.MedicineID)
                {
                    return false;
                }
            }
            storedMedicine.Add(newMedicine);
            WriteToFile(storedMedicine);
            return true;
        }

        public Boolean Update(Medicine editedMedicine)
        {
            var storedMedicine = ReadFromFile();
            foreach (var medicine in storedMedicine)
            {
                if (medicine.MedicineID == editedMedicine.MedicineID)
                {
                    medicine.Manufacturer = editedMedicine.Manufacturer;
                    medicine.Name = editedMedicine.Name;
                    medicine.Packaging = editedMedicine.Packaging;
                    medicine.Status = editedMedicine.Status;
                    medicine.ingridient = editedMedicine.ingridient;
                    medicine.Condition = editedMedicine.Condition;
                    medicine.ReplacementMedicine = editedMedicine.ReplacementMedicine;
                    WriteToFile(storedMedicine);
                    return true;
                }
            }
            return false;
        }

        public Medicine GetOne(int id)
        {
            var allMedicine = GetAll();
            foreach (var medicine in allMedicine)
            {
                if (medicine.MedicineID.Equals(id))
                {
                    return medicine;
                }
            }
            return null;
        }

        public Boolean Delete(int id)
        {
            var storedMedicine = ReadFromFile();
            foreach (var medicine in storedMedicine)
            {
                if (medicine.MedicineID == id && medicine.IsDeleted == false)
                {
                    medicine.IsDeleted = true;
                    WriteToFile(storedMedicine);
                    return true;
                }
            }
            return false;
        }

        public List<Medicine> ReadFromFile()
        {
            try
            {
                var jsonFromFile = File.ReadAllText(FileName);
                var allMedicine = JsonConvert.DeserializeObject<List<Medicine>>(jsonFromFile);
                return allMedicine;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return new List<Medicine>();
        }

        private void WriteToFile(List<Medicine> medicine)
        {
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(medicine, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch
            {
                MessageBox.Show("Neuspesno pisanje u fajl" + this.FileName + "!");
            }
        }

        public int GenerateNextId()
        {
            var storedMedicine = ReadFromFile();
            return storedMedicine.Count;
        }

        public List<Medicine> GetAwaiting()
        {
            var awaitingMedicine = new List<Medicine>();
            var storedMedicine = ReadFromFile();
            foreach (var medicine in storedMedicine)
            {
                if (medicine.IsDeleted == false && medicine.Status == MedicineStatus.awaiting)
                {
                    awaitingMedicine.Add(medicine);
                }
            }
            return awaitingMedicine;
        }

        public List<Medicine> GetApproved()
        {
            var approvedMedicine = new List<Medicine>();
            var storedMedicine = ReadFromFile();
            foreach (var medicine in storedMedicine)
            {
                if (medicine.IsDeleted == false && medicine.Status == MedicineStatus.approved)
                {
                    approvedMedicine.Add(medicine);
                }
            }
            return approvedMedicine;
        }
    }
}






/*using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
    public class MedicineFileRepository
    {

        public MedicineFileRepository()
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
}*/
