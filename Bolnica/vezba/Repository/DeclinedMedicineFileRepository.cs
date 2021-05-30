using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
    public class DeclinedMedicineFileRepository
    {
        public String FileName { get; set; }

        public DeclinedMedicineFileRepository()
        {
            FileName = "../../odbijenilekovi.json";
        }

        public List<DeclinedMedicine> GetAll()
        {
            var allDeclinedMedicine = new List<DeclinedMedicine>();
            var declinedMedicineFromFile = ReadFromFile();
            foreach (var declinedMedicine in declinedMedicineFromFile)
            {
                if (declinedMedicine.IsDeleted == false)
                {
                    allDeclinedMedicine.Add(declinedMedicine);
                }
            }
            return allDeclinedMedicine;
        }

        public Boolean Save(DeclinedMedicine newDeclinedMedicine)
        {
            var storedDeclinedMedicine = ReadFromFile();
            foreach (var declinedMedicine in storedDeclinedMedicine)
            {
                if (declinedMedicine.DeclinedMedicineID == newDeclinedMedicine.DeclinedMedicineID)
                {
                    return false;
                }
            }
            storedDeclinedMedicine.Add(newDeclinedMedicine);
            WriteToFile(storedDeclinedMedicine);
            return true;
        }

        public Boolean Update(DeclinedMedicine editedDeclinedMedicine)
        {
            var storedDeclinedMedicine = ReadFromFile();
            foreach (var declinedMedicine in storedDeclinedMedicine)
            {
                if (declinedMedicine.DeclinedMedicineID == editedDeclinedMedicine.DeclinedMedicineID)
                {
                    declinedMedicine.Medicine = editedDeclinedMedicine.Medicine;
                    declinedMedicine.Description = editedDeclinedMedicine.Description;
                    WriteToFile(storedDeclinedMedicine);
                    return true;
                }
            }
            return false;
        }

        public DeclinedMedicine GetOne(int id)
        {
            var allDeclinedMedicine = GetAll();
            foreach (var declinedMedicine in allDeclinedMedicine)
            {
                if (declinedMedicine.DeclinedMedicineID.Equals(id))
                {
                    return declinedMedicine;
                }
            }
            return null;
        }

        public Boolean Delete(int id)
        {
            var storedDeclinedMedicine = ReadFromFile();
            foreach (var declinedMedicine in storedDeclinedMedicine)
            {
                if (declinedMedicine.DeclinedMedicineID == id && declinedMedicine.IsDeleted == false)
                {
                    declinedMedicine.IsDeleted = true;
                    WriteToFile(storedDeclinedMedicine);
                    return true;
                }
            }
            return false;
        }

        public List<DeclinedMedicine> ReadFromFile()
        {
            List<DeclinedMedicine> dm = new List<DeclinedMedicine>();
            try
            {
                var jsonFromFile = File.ReadAllText(this.FileName);
                List<DeclinedMedicine> allDeclinedMedicine = JsonConvert.DeserializeObject<List<DeclinedMedicine>>(jsonFromFile);
                return allDeclinedMedicine;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return dm;
        }

        private void WriteToFile(List<DeclinedMedicine> declinedMedicine)
        {
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(declinedMedicine, Formatting.Indented);
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
            List<DeclinedMedicine> storedDeclinedMedicine = ReadFromFile();
            return storedDeclinedMedicine.Count;
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
    public class DeclinedMedicineFileRepository
    {

        public DeclinedMedicineFileRepository()
        {
            this.FileName = "../../odbijenilekovi.json";
        }

        public List<DeclinedMedicine> GetAll()
        {
            List<DeclinedMedicine> ms = new List<DeclinedMedicine>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<DeclinedMedicine> declinedMedicineList = JsonConvert.DeserializeObject<List<DeclinedMedicine>>(jsonFromFile);

                for (int i = 0; i < declinedMedicineList.Count; i++)
                {
                    if (declinedMedicineList[i].IsDeleted == false)
                    {
                        ms.Add(declinedMedicineList[i]);
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

        public Boolean Save(DeclinedMedicine declinedMedicine)
        {
            List<DeclinedMedicine> declinedMedicineList = GetAll();
            declinedMedicineList.Add(declinedMedicine);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(declinedMedicineList, Formatting.Indented);
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

        public Boolean Update(DeclinedMedicine declinedMedicine)
        {
            List<DeclinedMedicine> declinedMedicineList = GetAll();
            for (int i = 0; i < declinedMedicineList.Count; i++)
            {
                if (declinedMedicineList[i].DeclinedMedicineID.Equals(declinedMedicine.DeclinedMedicineID))
                {
                    declinedMedicineList[i].Medicine = declinedMedicine.Medicine;
                    declinedMedicineList[i].Description = declinedMedicine.Description;
                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(declinedMedicineList, Formatting.Indented);
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

        public DeclinedMedicine GetOne(int id)
        {
            List<DeclinedMedicine> declinedMedicineList = GetAll();
            for (int i = 0; i < declinedMedicineList.Count; i++)
            {
                if (declinedMedicineList[i].DeclinedMedicineID.Equals(id))
                {
                    return declinedMedicineList[i];
                }
            }
            return null;
        }

        public Boolean Delete(int id)
        {
            List<DeclinedMedicine> declinedMedicineList = GetAll();
            for (int i = 0; i < declinedMedicineList.Count; i++)
            {
                if (declinedMedicineList[i].DeclinedMedicineID.Equals(id))
                {
                    declinedMedicineList[i].IsDeleted = true;


                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(declinedMedicineList, Formatting.Indented);
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

        public List<DeclinedMedicine> Load()
        {
            List<DeclinedMedicine> dm = new List<DeclinedMedicine>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<DeclinedMedicine> declinedMedicineList = JsonConvert.DeserializeObject<List<DeclinedMedicine>>(jsonFromFile);
                return declinedMedicineList;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return dm;
        }

        public int generateNextId()
        {
            List<DeclinedMedicine> list = Load();
            return list.Count;
        }

        public String FileName { get; set; }
    }
}*/
