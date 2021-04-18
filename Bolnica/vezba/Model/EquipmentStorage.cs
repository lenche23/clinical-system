// File:    EquipmentStorage.cs
// Author:  graho
// Created: 16 April 2021 17:12:53
// Purpose: Definition of Class EquipmentStorage

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Model
{
   public class EquipmentStorage
   {
        public EquipmentStorage()
        {
            this.FileName = "../../oprema.json";
        }

        public String FileName { get; set; }

        public List<Equipment> GetAll()
      {
            List<Equipment> es = new List<Equipment>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Equipment> equipmentList = JsonConvert.DeserializeObject<List<Equipment>>(jsonFromFile);

                for (int i = 0; i < equipmentList.Count; i++)
                {
                    if (equipmentList[i].IsDeleted == false)
                    {
                        es.Add(equipmentList[i]);
                    }
                }
                return es;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return es;
        }
      
      public Boolean Save(Equipment eq)
      {
            List<Equipment> equipmentList = GetAll();
            equipmentList.Add(eq);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(equipmentList, Formatting.Indented);
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

        public List<Equipment> Load()
        {
            List<Equipment> es = new List<Equipment>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Equipment> equipmentList = JsonConvert.DeserializeObject<List<Equipment>>(jsonFromFile);
                return equipmentList;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return es;
        }

        public Boolean Update(Equipment eq)
      {
            List<Equipment> equimentList = Load();
            for (int i = 0; i < equimentList.Count; i++)
            {
                if (equimentList[i].Id.Equals(eq.Id))
                {
                    equimentList[i].Name = eq.Name;
                    equimentList[i].Quantity = eq.Quantity;
                    equimentList[i].Type = eq.Type;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(equimentList, Formatting.Indented);
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
      
      public Equipment GetOne(int id)
      {
            List<Equipment> equipmentList = GetAll();
            for (int i = 0; i < equipmentList.Count; i++)
            {
                if (equipmentList[i].Id.Equals(id))
                {
                    return equipmentList[i];
                }
            }
            return null;
        }
      
      public Boolean Delete(int id)
      {
            List<Equipment> equipmentList = Load();
            for (int i = 0; i < equipmentList.Count; i++)
            {
                if (equipmentList[i].Id.Equals(id))
                {
                    equipmentList[i].IsDeleted = true;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(equipmentList, Formatting.Indented);
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
            }
            return false;
        }
      
     

    }
}