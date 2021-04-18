using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
    public class AppointmentStorage
    {
        public String FileName { get; set; }

        public AppointmentStorage() 
        {
            this.FileName = "../../appointments.json";
        }

        public List<Appointment> Load()
        {
            List<Appointment> ps = new List<Appointment>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonFromFile);
                return appointments;
            }
            catch {}
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return ps;
        }
        public List<Appointment> GetAll()
        {          
            List<Appointment> appointments = new List<Appointment>();
            List<Appointment> appointments1 = Load();
            for (int i = 0; i < appointments1.Count; i++)
            {
                if (appointments1[i].IsDeleted == false)
                {
                    appointments.Add(appointments1[i]);
                }
            }
            return appointments;
        }

        public Boolean Save(Appointment appointment)
        {

            List<Appointment> appointments = Load();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId.Equals(appointment.AppointentId))
                {
                    return false;
                }
            }
            appointments.Add(appointment);
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e) {}
            return true;
        }

        public Boolean Update(Appointment appointment)
        {
            List<Appointment> appointments = Load();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId.Equals(appointment.AppointentId) && appointments[i].IsDeleted == false)
                {
                    appointments[i].StartTime = appointment.StartTime;
                    appointments[i].DurationInMunutes = appointment.DurationInMunutes;
                    appointments[i].ApointmentDescription = appointment.ApointmentDescription;
                    appointments[i].Patient = appointment.Patient;
                    appointments[i].Room = appointment.Room;
                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e) {}
                }
            }
            return false;
        }

        public Appointment GetOne(int id)
        {
            List<Appointment> appointments = Load();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId == id && appointments[i].IsDeleted == false)
                {
                    return appointments[i];
                }
            }
            return null;
        }

        public Boolean Delete(int id)
        {
            List<Appointment> list = Load();
            for (int i = 0; i < list.Count; i++) 
            {
                if (list[i].AppointentId == id && list[i].IsDeleted == false)
                {
                    list[i].IsDeleted = true;
                
                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(list, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                            writer.Write(jsonToFile);
                        
                    }
                    catch (Exception e) {}
                    return true;
                }
            }
            return false;
        }

        public int generateNextId()
        {
            List<Appointment> list = Load();
            return list.Count;
        }

    }
}