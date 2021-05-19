// File:    RoomInventoryFileRepository.cs
// Author:  graho
// Created: 26 April 2021 15:31:43
// Purpose: Definition of Class RoomInventoryFileRepository

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
   public class RoomInventoryFileRepository
   {

        public RoomInventoryFileRepository()
        {
            this.FileName = "../../room_inventory.json";
        }

        public String FileName { get; set; }

        public List<RoomInventory> GetAll()
      {
            List<RoomInventory> ris = new List<RoomInventory>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<RoomInventory> roomInventoryList = JsonConvert.DeserializeObject<List<RoomInventory>>(jsonFromFile);

                for (int i = 0; i < roomInventoryList.Count; i++)
                {
                   if (roomInventoryList[i].IsDeleted == false)
                    {
                        ris.Add(roomInventoryList[i]);
                    }
                }
                return ris;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return ris;
        }
      
      public Boolean Save(RoomInventory inventory)
      {
            List<RoomInventory> roomInventoryList = GetAll();
            roomInventoryList.Add(inventory);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(roomInventoryList, Formatting.Indented);
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
      
      public Boolean Update(RoomInventory inventory)
      {
            List<RoomInventory> roomInventoryList = GetAll();
            for (int i = 0; i < roomInventoryList.Count; i++)
            {
                if (roomInventoryList[i].Id.Equals(inventory.Id))
                {
                    roomInventoryList[i].Quantity = inventory.Quantity;
                    roomInventoryList[i].equipment = inventory.equipment;
                    roomInventoryList[i].room = inventory.room;
                    roomInventoryList[i].StartTime = inventory.StartTime;
                    roomInventoryList[i].EndTime = inventory.EndTime;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(roomInventoryList, Formatting.Indented);
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
      
      public RoomInventory GetOne(int id)
      {
            List<RoomInventory> roomInventoryList = GetAll();
            for (int i = 0; i < roomInventoryList.Count; i++)
            {
                if (roomInventoryList[i].Id.Equals(id))
                {
                    return roomInventoryList[i];
                }
            }
            return null;
        }
      
      public Boolean Delete(int id)
      {
            List<RoomInventory> roomInventoryList = GetAll();
            for (int i = 0; i < roomInventoryList.Count; i++)
            {
                if (roomInventoryList[i].Id.Equals(id))
                {
                    roomInventoryList[i].IsDeleted = true;


                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(roomInventoryList, Formatting.Indented);
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
      
      public List<RoomInventory> Load()
      {
            List<RoomInventory> ri = new List<RoomInventory>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<RoomInventory> roomInventoryList = JsonConvert.DeserializeObject<List<RoomInventory>>(jsonFromFile);
                return roomInventoryList;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return ri;
        }
      
      public int GenerateNextId()
      {
            List<RoomInventory> list = Load();
            return list.Count;
        }
    }
}