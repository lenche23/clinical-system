using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model
{
    public class DeclinedMedicineStorage
    {

        public DeclinedMedicineStorage()
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
}
