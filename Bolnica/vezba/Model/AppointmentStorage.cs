using System;
using System.Collections.Generic;

namespace Model
{
    public class AppointmentStorage
    {
        public String FileName { get; set; }
        public List<Appointment> apps;      //OTVARAS FAJL,SA GETALL ISCITRAVAS JEDNU LINIJU ISPROCESUIRAS CEO APPOINTMENT 

        public AppointmentStorage() 
        {
            apps = new List<Appointment>();
            Appointment app1 = new Appointment { StartTime = DateTime.Now, DurationInMunutes = 45, AppointentId = 1, ApointmentDescription = "eye examination", Doctor = null, IsDeleted = false, patient = null };
            Appointment app2 = new Appointment { StartTime = new DateTime(DateTime.Now.Year, 1, 1), DurationInMunutes = 15, AppointentId = 2, ApointmentDescription = "ear examination", Doctor = null, IsDeleted = false, patient = null };
            apps.Add(app1);
            apps.Add(app2);
        }

        public List<Appointment> GetAll()
        {
            //ovde file ili directory za proveru da li postoji fajl koji smo prosledili i sacuivali u fajl location i prolazimo kroz listu iz fajla i transformisali je u apps objekat
            return apps;
            //throw new NotImplementedException();
        }

        public void Save(Appointment appointment)
        {
            //apps.Add(appointment);
            throw new NotImplementedException();
        }

        public Boolean Update(Appointment appointment)
        {
            //for (int i = 0; i < apps.Count; i++) {
            //    if (apps[i].AppointentId == appointment.AppointentId) {
            //        apps[i] = appointment;
            //    }
            //}
            throw new NotImplementedException();
        }

        public Appointment GetOne(int id)
        {
            //for (int i = 0; i < apps.Count; i++) {
            //    if (apps[i].AppointentId == id)
            //        return apps[i];
            //}
             
            throw new NotImplementedException();
        }

    }
}