using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
   public class EventsLogStorage
   {
        public String FileName { get; set; }

        public EventsLogStorage()
        {
            this.FileName = "../../events_log.json";
        }

        public List<EventsLog> GetAll()
        {
            List<EventsLog> logs = new List<EventsLog>();
            List<EventsLog> logs1 = Load();
            /*for (int i = 0; i < logs1.Count; i++)
            {
                if (logs1[i].IsDeleted == false)
                {
                    logs.Add(logs1[i]);
                }
            }*/
            return logs1;
        }
      
        public Boolean Save(EventsLog log)
        {
            List<EventsLog> logs = Load();
            logs.Add(log);
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(logs, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e) { }

            return true;
        }
      
        public Boolean Update(EventsLog log)
        {
            throw new NotImplementedException();
        }
      
        public EventsLog GetOne(String patientJmbg)
        {
            throw new NotImplementedException();
        }
      
        public Boolean Delete(String patientJmbg)
        {
            throw new NotImplementedException();
        }
      
        public List<EventsLog> Load()
        {
            List<EventsLog> logs = new List<EventsLog>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<EventsLog> logs1 = JsonConvert.DeserializeObject<List<EventsLog>>(jsonFromFile);
                return logs1;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return logs;
        }
    }
}