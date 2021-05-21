using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Appointment : INotifyPropertyChanged
    {
        public DateTime StartTime { get; set; }
        public int DurationInMunutes { get; set; }
        public String ApointmentDescription { get; set; }
        public int AppointentId { get; set; }
        public Boolean IsDeleted { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }
        public Patient Patient { get; set; }
        public Boolean IsEmergency { get; set; }

        public Appointment(int id, Patient patient, Doctor doctor, Room room, DateTime startTime, int duration, string apDesc, Boolean IsEmergency = false)
        {
            AppointentId = id;
            Patient = patient;
            Doctor = doctor;
            Room = room;
            StartTime = startTime;
            DurationInMunutes = duration;
            ApointmentDescription = apDesc;
            IsDeleted = false;
            this.IsEmergency = IsEmergency;
        }

        public Appointment(Doctor doctor, DateTime startTime, Patient patient) {
            RoomStorage rs = new RoomStorage();
            List<Room> rooms = rs.GetAll();
            Room room = rooms[0];
            if (rooms.Count == 0)
            {
                room = null;
            }
            this.DurationInMunutes = 15;
            this.ApointmentDescription = "Pregled kod lekara opste prakse.";
            this.IsDeleted = false;
            this.Doctor = doctor;
            this.StartTime = startTime;
            this.Room = room;
            this.Patient = patient;
        }

        public DateTime EndTime
        {
            get
            {
                return StartTime.AddMinutes(DurationInMunutes);
            }
        }

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
            set
            {
                OnPropertyChanged("RoomName");
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
            set
            {
                OnPropertyChanged("DoctorName");
            }
        }

        public String AppointmentDescription
        {
            get => ApointmentDescription;
            set
            {
                ApointmentDescription = value;
                OnPropertyChanged("AppointmentDescription");
            }
        }

        public DateTime StartTimee
        {
            get => StartTime;
            set
            {
                StartTime = value;
                OnPropertyChanged("StartTimee");
            }
            
        }

        public Appointment() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
