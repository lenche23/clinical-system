using System;
using System.Collections.Generic;

namespace Model
{
   public class DoctorStorage
   {
      public List<Doctor> GetAll()
      {
            return Load();
      }
      
      public Boolean Save(Doctor d)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(Doctor d)
      {
         throw new NotImplementedException();
      }
      
      public Doctor GetOne(String jmbg)
      {
            List<Doctor> doctors = GetAll();
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Jmbg.Equals(jmbg))
                {
                    return doctors[i];
                }
            }
            return null;
        }
      
      public Boolean Delete(String jmbg)
      {
         throw new NotImplementedException();
      }
      
      public List<Doctor> Load()
      {
            List<Doctor> doctors = new List<Doctor>();
            Speciality s1 = new Speciality("Oftalmolog");
            Doctor d1 = new Doctor("Pera", "Peric", "1541546542644", DateTime.Now, Sex.male, "0621846712", "neka ulica 45, Novi Sad", "pera@gmail.com", "007123352", 90000, s1, "pera", "perica");
            doctors.Add(d1); 
            Speciality s2 = new Speciality("Kardiolog");
            Doctor d2 = new Doctor("Dana", "Dimitrijevic", "0104998253658", DateTime.Now, Sex.female, "0621975732", "neka ulica 62, Novi Sad", "dana@gmail.com", "008996452", 120000, s2, "dana", "danica");
            doctors.Add(d2);
            Doctor d3 = new Doctor("Darko", "Torbica", "1708962324890", DateTime.Now, Sex.male, "0665813272", "neka ulica 38, Novi Sad", "darko@gmail.com", "007155721", 70000, null, "darko", "dare");
            doctors.Add(d3);
            Speciality s4 = new Speciality("Stomatolog");
            Doctor d4 = new Doctor("Milica", "Milicevic", "2205972659017", DateTime.Now, Sex.female, "06364789110", "neka ulica 98, Novi Sad", "milica@gmail.com", "008100252", 100000, s4, "milica", "mica");
            doctors.Add(d4);
            return doctors;

        }
      
      public String FileName { get; set; }

    }
}