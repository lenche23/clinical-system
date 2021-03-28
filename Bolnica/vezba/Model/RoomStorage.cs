using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Model
{
    public class RoomStorage
    {
        public RoomStorage()
        {
            this.FileName = "../../sobe.json";
        }

        public List<Room> GetAll()
        {
            List<Room> rs = new List<Room>();

            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(jsonFromFile);

                for(int i=0; i<rooms.Count; i++)
                {
                    if(rooms[i].IsDeleted == false)
                    {
                        rs.Add(rooms[i]);
                    }
                }
                return rs;
            }
            catch
            {

            }

            MessageBox.Show("Neuspesno ucitavanje iz fajla" + this.FileName + "!");
            return rs;
        }

        public void Save(Room room)
        {
            List<Room> rooms = GetAll();
            rooms.Add(room);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(rooms, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch(Exception e)
            {

            }
        }

        public Boolean Update(Room room)
        {
            List<Room> rooms = GetAll();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomNumber.Equals(room.RoomNumber))
                {
                    rooms[i].RoomFloor = room.RoomFloor;
                    rooms[i].RoomType = room.RoomType;


                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(rooms, Formatting.Indented);
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

        public Room GetOne(int number)
        {
            List<Room> rooms = GetAll();
            for(int i=0; i<rooms.Count; i++)
            {
                if(rooms[i].RoomNumber.Equals(number))
                {
                    return rooms[i];
                }
            }
            return null;
        }

        public Boolean Delete(int number)
        {
            List<Room> rooms = GetAll();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomNumber.Equals(number))
                {
                    rooms[i].IsDeleted = true;


                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(rooms, Formatting.Indented);
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


        public String FileName { get; set; }

    }
}