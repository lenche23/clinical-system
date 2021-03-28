using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class AppointmentStorage
    {
        public String FileName { get; set; }

        public AppointmentStorage() 
        {
            this.FileName = "appointments.json";
        }

        public List<Appointment> GetAll()
        {          
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Appointment> appointments1 = JsonConvert.DeserializeObject<List<Appointment>>(jsonFromFile);

                for (int i = 0; i < appointments1.Count; i++)
                {
                    if (appointments1[i].IsDeleted == false)
                    {
                        appointments.Add(appointments1[i]);
                    }
                }
                return appointments;
            }
            catch (Exception e)
            { 
            }
            return appointments;
        }

        public void Save(Appointment appointment)
        {
            List<Appointment> appointments = GetAll();
            appointments.Add(appointment);
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }               
            }
            catch (Exception e)
            {
            }
        }

        public Boolean Update(Appointment appointment)
        {
            List<Appointment> list = GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AppointentId.Equals(appointment.AppointentId))
                {
                    list[i].ApointmentDescription = appointment.ApointmentDescription;
                    list[i].AppointentId = appointment.AppointentId;
                    list[i].Doctor = appointment.Doctor;
                    list[i].DurationInMunutes = appointment.DurationInMunutes;
                    list[i].IsDeleted = appointment.IsDeleted;
                    list[i].Patient = appointment.Patient;
                    list[i].Room = appointment.Room;
                    list[i].StartTime = appointment.StartTime;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(list, Formatting.Indented);
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

        public Appointment GetOne(int id)
        {
            List<Appointment> appointments = GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId == id)
                {
                    return appointments[i];
                }
            }
            return null;
        }

        public Boolean Delete(int appointment)
        {
            List<Appointment> list = GetAll();
            for (int i = 0; i < list.Count; i++) 
            {
                if (list[i].AppointentId == appointment)
                {
                    list[i].IsDeleted = true;
                    return true;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(list, Formatting.Indented);
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

    }
}