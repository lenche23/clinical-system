using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
    public class AppointmentStorage
    {

        public AppointmentStorage()
        {
            this.FileName = "termini.json";
        }

        public List<Appointment> GetAll()
        {
            List<Appointment> aps = new List<Appointment>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonFromFile);

                for (int i = 0; i < appointments.Count; i++)
                {
                    if (appointments[i].IsDeleted == false)
                    {
                        aps.Add(appointments[i]);
                    }
                }
                return aps;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return aps;
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
            List<Appointment> appointments = GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId.Equals(appointment.AppointentId))
                {
                    appointments[i].StartTime = appointment.StartTime;
                    appointments[i].DurationInMunutes = appointment.DurationInMunutes;
                    appointments[i].ApointmentDescription = appointment.ApointmentDescription;
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
            }
            return false;
        }

        public Appointment GetOne(int id)
        {
            List<Appointment> appointments = GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId.Equals(id))
                {
                    return appointments[i];
                }
            }
            return null;
        }

        public Boolean Delete(int id) 
        {
            List<Appointment> appointments = GetAll();
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AppointentId.Equals(id))
                {
                    appointments[i].IsDeleted = true;
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
            }
            return false;
        }

        public String FileName { get; set; }
    }
}