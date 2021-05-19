using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
   public class EventsLogFileRepository
   {
        public String FileName { get; set; }

        public EventsLogFileRepository()
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
            List<EventsLog> logs = new List<EventsLog>();
            logs = Load();
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
            List<EventsLog> logs = Load();
            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i].PatientJmbg.Equals(log.PatientJmbg))
                {
                    logs[i].PatientJmbg = log.PatientJmbg;
                    logs[i].EventDates = log.EventDates;
                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(logs, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e) { }
                }
            }
            return false;
        }
      
        public EventsLog GetOne(String patientJmbg)
        {
            List<EventsLog> logs = Load();
            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i].PatientJmbg == patientJmbg)
                {
                    return logs[i];
                }
            }
            return null;
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
                List<EventsLog> logs1 = new List<EventsLog>();
                logs1 = JsonConvert.DeserializeObject<List<EventsLog>>(jsonFromFile);
                if (logs1 == null)
                    return new List<EventsLog>();
                return logs1;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return logs;
        }
    }
}