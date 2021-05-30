using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace vezba.Repository
{
    public class AppointmentFileRepository: IAppointmentRepository
    {
        public String FileName { get; set; }

        public AppointmentFileRepository()
        {
            this.FileName = "../../appointments.json";
        }

        
        public List<Appointment> GetAll()
        {          
            List<Appointment> appointments = new List<Appointment>();
            List<Appointment> storedAppointments = ReadFromFile();
            for (int i = 0; i < storedAppointments.Count; i++)
            {
                if (storedAppointments[i].IsDeleted == false)
                {
                    appointments.Add(storedAppointments[i]);
                }
            }
            return appointments;
        }
        
        public Boolean Save(Appointment newAppointment)
        {
            newAppointment.AppointentId = GenerateNextId();
            List<Appointment> storedAppointments = ReadFromFile();
            for (int i = 0; i < storedAppointments.Count; i++)
            {
                if (storedAppointments[i].AppointentId.Equals(newAppointment.AppointentId))
                    return false;
            }
            storedAppointments.Add(newAppointment);
            WriteToFile(storedAppointments);
            return true;
        }

        public Boolean Update(Appointment editedAppointment)
        {
            List<Appointment> storedAppointments = ReadFromFile();
            foreach (Appointment appointment in storedAppointments)
            {
                if (appointment.AppointentId.Equals(editedAppointment.AppointentId) && appointment.IsDeleted == false)
                {
                    appointment.StartTime = editedAppointment.StartTime;
                    appointment.DurationInMunutes = editedAppointment.DurationInMunutes;
                    appointment.ApointmentDescription = editedAppointment.ApointmentDescription;
                    appointment.Patient = editedAppointment.Patient;
                    appointment.Room = editedAppointment.Room;
                    appointment.Doctor = editedAppointment.Doctor;
                    appointment.IsEmergency = editedAppointment.IsEmergency;
                    WriteToFile(storedAppointments);
                    return true;
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
                    return appointments[i];
            }
            return null;
        }

        public Boolean Delete(int id)
        {
            List<Appointment> storedAppointments = ReadFromFile();
            for (int i = 0; i < storedAppointments.Count; i++) 
            {
                if (storedAppointments[i].AppointentId == id && storedAppointments[i].IsDeleted == false)
                {
                    storedAppointments[i].IsDeleted = true;
                    WriteToFile(storedAppointments);
                    return true;
                }
            }
            return false;
        }
        private List<Appointment> ReadFromFile()
        {
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonFromFile);
                return appointments;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return new List<Appointment>();
        }

        private void WriteToFile(List<Appointment> appointments)
        {
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
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
            List<Appointment> appointments = ReadFromFile();
            return appointments.Count;
        }

    }
}