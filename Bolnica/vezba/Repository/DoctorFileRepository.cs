using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
    public class DoctorFileRepository
    {
        public String FileName { get; set; }

        public DoctorFileRepository()
        {
            this.FileName = "../../doctors.json";
        }

        public List<Doctor> GetAll()
        {
            List<Doctor> storedDoctors = ReadFromFile();
            List<Doctor> doctors = new List<Doctor>();
            for (int i = 0; i < storedDoctors.Count; i++)
            {
                if (storedDoctors[i].IsDeleted == false)
                    doctors.Add(storedDoctors[i]);
            }
            return doctors;
        }

        public List<Doctor> GetDoctorsWithSpeciality(Speciality speciality)
        {
            List<Doctor> doctors = GetAll();
            List<Doctor> doctorsWithSpeciality = new List<Doctor>();
            foreach (Doctor d in doctors)
            {
                if (d.Speciality.Name.Equals(speciality.Name))
                    doctorsWithSpeciality.Add(d);
            }
            return doctorsWithSpeciality;
        }

        public Doctor GetOne(String jmbg)
        {
            List<Doctor> doctors = GetAll();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Jmbg.Equals(jmbg))
                    return doctor;
            }
            return null;
        }
      
      
        private List<Doctor> ReadFromFile()
        {
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(jsonFromFile);
                return doctors;
            }
            catch { }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return new List<Doctor>();

            /*List<Doctor> doctors = new List<Doctor>();
            Speciality s1 = new Speciality("Oftalmolog");
            Doctor d1 = new Doctor("Pera", "Peric", "1541546542644", DateTime.Now, Sex.male, "0621846712", "neka ulica 45, Novi Sad", "pera@gmail.com", "007123352", 90000, s1, "pera", "perica");
            doctors.Add(d1); 
            Speciality s2 = new Speciality("Kardiolog");
            Doctor d2 = new Doctor("Dana", "Dimitrijevic", "0104998253658", DateTime.Now, Sex.female, "0621975732", "neka ulica 62, Novi Sad", "dana@gmail.com", "008996452", 120000, s2, "dana", "danica");
            doctors.Add(d2);
            Doctor d3 = new Doctor("Darko", "Torbica", "1708962324890", DateTime.Now, Sex.male, "0665813272", "neka ulica 38, Novi Sad", "darko@gmail.com", "007155721", 70000, s2, "darko", "dare");
            doctors.Add(d3);
            Speciality s4 = new Speciality("Stomatolog");
            Doctor d4 = new Doctor("Milica", "Milicevic", "2205972659017", DateTime.Now, Sex.female, "06364789110", "neka ulica 98, Novi Sad", "milica@gmail.com", "008100252", 100000, s4, "milica", "mica");
            doctors.Add(d4);
            Speciality s5 = new Speciality("Opsti");
            Doctor d5 = new Doctor("Jana", "Jankovic", "0611985621455", DateTime.Now, Sex.female, "06355589110", "neka ulica 12, Novi Sad", "jana@gmail.com", "007521352", 100000, s5, "jana", "jaca");
            doctors.Add(d5);
            WorkingHours wh1 = new WorkingHours(new DateTime(2021, 5, 24), Shift.firstShift);
            WorkingHours wh2 = new WorkingHours(new DateTime(2021, 5, 24), Shift.secondShift);
            WorkingHours wh3 = new WorkingHours(new DateTime(2021, 5, 17), Shift.secondShift);

            d1.WorkingSchedule.Add(wh2);
            d2.WorkingSchedule.Add(wh1);
            d3.WorkingSchedule.Add(wh1);
            d4.WorkingSchedule.Add(wh2);
            d5.WorkingSchedule.Add(wh1);

            d1.WorkingSchedule.Add(wh3);

            WriteToFile(doctors);*/

            //return doctors;

        }

        public Boolean Save(Doctor newDoctor)
        {
            List<Doctor> storedDoctors = ReadFromFile();

            foreach (Doctor doctor in storedDoctors)
            {
                if (doctor.Jmbg == newDoctor.Jmbg)
                    return false;
            }
            storedDoctors.Add(newDoctor);

            WriteToFile(storedDoctors);
            return true;
        }

        public Boolean Update(Doctor editedDoctor)
        {
            List<Doctor> storedDoctors = ReadFromFile();
            foreach (Doctor doctor in storedDoctors)
            {
                if (doctor.Jmbg == editedDoctor.Jmbg && doctor.IsDeleted == false)
                {
                    doctor.VacationDays = editedDoctor.VacationDays;
                    doctor.WorkingSchedule = editedDoctor.WorkingSchedule;
                    doctor.AvailableDaysOff = editedDoctor.AvailableDaysOff;

                    WriteToFile(storedDoctors);
                    return true;
                }
            }
            return false;
        }

        public Boolean Delete(String jmbg)
        {
            List<Doctor> storedDoctors = ReadFromFile();
            foreach (Doctor doctor in storedDoctors)
            {
                if (doctor.Jmbg == jmbg && doctor.IsDeleted == false)
                {
                    doctor.IsDeleted = true;
                    WriteToFile(storedDoctors);
                    return true;

                }
            }
            return false;
        }

        private void WriteToFile(List<Doctor> doctors)
        {
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(doctors, Formatting.Indented);
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

    }
}