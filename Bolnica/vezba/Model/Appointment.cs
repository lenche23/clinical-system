using System;

namespace Model
{
    public class Appointment
    {
        public Appointment(DateTime StartTime, int DurationInMinutes, String AppointmentDescription, int AppointmentId, Doctor Doctor, Room Room, Patient patient)
        {
            this.StartTime = StartTime;
            this.DurationInMunutes = DurationInMinutes;
            this.ApointmentDescription = AppointmentDescription;
            this.AppointentId = AppointmentId;
            this.Doctor = Doctor;
            this.Room = Room;
            this.patient = patient;
            this.IsDeleted = false;
        }

        public DateTime StartTime { get; set; }
        public int DurationInMunutes { get; set; }
        public String ApointmentDescription { get; set; }
        public int AppointentId { get; set; }
        public Boolean IsDeleted { get; set; }

        public Doctor Doctor { get; set; }
        public Room Room { get; set; }
        public Patient patient;

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (this.patient == null || !this.patient.Equals(value))
                {
                    if (this.patient != null)
                    {
                        Patient oldPatient = this.patient;
                        this.patient = null;
                        oldPatient.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.patient = value;
                        this.patient.AddAppointment(this);
                    }
                }
            }
        }

    }
}