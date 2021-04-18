using System;
using System.Collections.Generic;

namespace Model
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public int DurationInMunutes { get; set; }
        public String ApointmentDescription { get; set; }
        public int AppointentId { get; set; }
        public Boolean IsDeleted { get; set; }

        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public Patient Patient { get; set; }

        public Appointment(Doctor doctor, DateTime startTime, Patient patient) {
            Random rnd = new Random();
            RoomStorage rs = new RoomStorage();
            List<Room> rooms = rs.GetAll();
            Room room = rooms[0];
            if (rooms.Count == 0)
            {
                room = null;
            }

            this.DurationInMunutes = 15;
            this.ApointmentDescription = "Pregled kod lekara opšte prakse.";
            this.IsDeleted = false;
            this.Doctor = doctor;
            this.StartTime = startTime;
            this.Room = room;
            this.Patient = patient;
        }

        public Appointment() {   }

    }
}