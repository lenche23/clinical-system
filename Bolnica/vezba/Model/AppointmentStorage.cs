using System;
using System.Collections.Generic;

namespace Model
{
    public class AppointmentStorage
    {
        public String FileName { get; set; }

        public List<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Boolean Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Appointment GetOne(int id)
        {
            throw new NotImplementedException();
        }

    }
}