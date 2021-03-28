using System;
using System.Collections.Generic;

namespace Model
{
    public class AppointmentStorage
    {
        public String FileName { get; set; }
        private List<Appointment> appointments;
        private static AppointmentStorage instance = null;

        private AppointmentStorage()
        {
            //create Doctor, Room and patient
            appointments = new List<Appointment>();
            var patient1 = new Patient { Name = "Slobodan", Surname = "Milosevic" };
            var doctor1 = new Doctor { Name = "Stevan", Surname = "Rodic" };
            var room1 = new Room { RoomNumber = 12};
            var appointment1 = new Appointment {AppointentId = 0, StartTime = new DateTime(2021, 04, 23, 10, 0, 0), DurationInMunutes = 30, ApointmentDescription = "Ceprkanje Bulje", IsDeleted = false, Doctor = doctor1, Patient = patient1, Room = room1 };
            appointments.Add(appointment1);
        }

        public static AppointmentStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentStorage();
                }
                return instance;
            }
        }

        public List<Appointment> GetAll()
        {
            return appointments;
        }

        public void Save(Appointment appointment)
        {
            appointments.Add(appointment);
        }

        public Boolean Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Appointment GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Appointment appointment) 
        {
            appointments.Remove(appointment);
        }

    }
}