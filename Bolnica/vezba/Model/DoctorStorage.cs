using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class DoctorStorage
    {
        public String FileName { get; set; }

        /*        public DoctorStorage()
                {
                    this.FileName = "../../doctors.json";
                }*/

        public List<Doctor> GetAll()
        {
                /*List<Doctor> doctors = new List<Doctor>();
                try
                {
                    String jsonFromFile = File.ReadAllText(this.FileName);
                    List<Doctor> doctors1 = JsonConvert.DeserializeObject<List<Doctor>>(jsonFromFile);

                    for (int i = 0; i < doctors1.Count; i++)
                    {
                        if (doctors1[i].IsDeleted == false)
                        {
                            doctors.Add(doctors1[i]);
                        }
                    }
                    return doctors;
                }
                catch (Exception e)
                {
                }
                return doctors;*/
            

            return Load();
        }

        public Boolean Save(Doctor d)
        {
                List<Doctor> doctors = Load();

                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].Jmbg.Equals(d.Jmbg) && doctors[i].IsDeleted == false)
                    {
                        return false;
                    }
                }
                doctors.Add(d);
                try
                {
                    var jsonToFile = JsonConvert.SerializeObject(doctors, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.FileName))
                    {
                        writer.Write(jsonToFile);
                    }
                }
                catch (Exception e)
                {

                }
                return true;
            
        }

        public Boolean Update(Doctor p)
        {
                List<Doctor> doctors = Load();
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].Jmbg.Equals(p.Jmbg))
                    {
                        doctors[i].Name = p.Name;
                        doctors[i].Surname = p.Surname;
                        doctors[i].DateOfBirth = p.DateOfBirth;
                        doctors[i].Sex = p.Sex;
                        doctors[i].PhoneNumber = p.PhoneNumber;
                        doctors[i].Adress = p.Adress;
                        doctors[i].IdCard = p.IdCard;
                        doctors[i].Email = p.Email;
                        doctors[i].Username = p.Username;
                        doctors[i].Password = p.Password;
                        doctors[i].Jmbg = p.Jmbg;
                        doctors[i].IsDeleted = false;
                        doctors[i].Type = UserType.doctor;

                        try
                        {
                            var jsonToFile = JsonConvert.SerializeObject(doctors, Formatting.Indented);
                            using (StreamWriter writer = new StreamWriter(this.FileName))
                            {
                                writer.Write(jsonToFile);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                return false;
            
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
                List<Doctor> doctors = Load();
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].Jmbg.Equals(jmbg))
                    {
                        doctors[i].IsDeleted = true;

                        try
                        {
                            var jsonToFile = JsonConvert.SerializeObject(doctors, Formatting.Indented);
                            using (StreamWriter writer = new StreamWriter(this.FileName))
                            {
                                writer.Write(jsonToFile);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                        return true;
                    }
                }
                return false;
            
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

            /*List<Doctor> doc = new List<Doctor>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(jsonFromFile);
                return doctors;
            }
            catch
            {

            }
            return doc;*/
        }

    }
    }
