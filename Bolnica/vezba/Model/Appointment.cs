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
        // public Patient patient;

        public Patient Patient { get; set; }


        public Appointment(int id, Patient patient, Doctor doctor, Room room, DateTime startTime, int duration, string apDesc)
        {
            AppointentId = id;
            //this.patient = patient;
            Patient = patient;
            Doctor = doctor;
            Room = room;
            StartTime = startTime;
            DurationInMunutes = duration;
            ApointmentDescription = apDesc;
            IsDeleted = false;
        }

        /*public Patient Patient
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
        }*/

        public String PatientName
        {
            get
            {
                if (Patient != null)
                    return (Patient.Name + " " + Patient.Surname);
                else
                    return "";
            }
        }
        public String RoomName
        {
            get
            {
                if (Room != null)
                    return Convert.ToString(Room.RoomNumber);
                else
                    return "";
            }
        }
        public String DoctorName
        {
            get
            {
                if (Doctor != null)
                    return (Doctor.Name + " " + Doctor.Surname);
                else 
                    return "";
                
                
            }
        }
    }
}