using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
    public class PatientStorage
    {
        //private List<Patient> patient = new List<Patient>();

        public PatientStorage()
        {
            this.FileName = "pacijenti.json";
        }
        public List<Patient> GetAll()
        {
            List<Patient> ps = new List<Patient>();
            try 
            { 
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(jsonFromFile);
                
                for (int i = 0; i < patients.Count; i++)
                {
                    if (patients[i].IsDeleted == false)
                    {
                        ps.Add(patients[i]);
                    }
                }
                return ps;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return ps;
        }

        public void Save(Patient p)
        {
            List<Patient> patients = GetAll();
            patients.Add(p);

            try
            {
                var jsonToFile = JsonConvert.SerializeObject(patients, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e)
            {

            }
        }

        public Boolean Update(Patient p)
        {
            List<Patient> patients = GetAll();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Jmbg.Equals(p.Jmbg))
                {
                    patients[i].Name = p.Name;
                    patients[i].Surname = p.Surname;
                    patients[i].DateOfBirth = p.DateOfBirth;
                    patients[i].Sex = p.Sex;
                    patients[i].PhoneNumber = p.PhoneNumber;
                    patients[i].Adress = p.Adress;
                    patients[i].IdCard = p.IdCard;
                    patients[i].Email = p.Email;
                    patients[i].EmergencyContact = p.EmergencyContact;
                    patients[i].MedicalRecord = p.MedicalRecord;
                    patients[i].Username = p.Username;
                    patients[i].IsGuest = p.IsGuest;
                    patients[i].Password = p.Password;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(patients, Formatting.Indented);
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

        public Patient GetOne(String jmbg)
        {
            List<Patient> patients = GetAll();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Jmbg.Equals(jmbg))
                {
                    return patients[i];
                }
            }
            return null;
        }

        public Boolean Delete(string jmbg)
        {
            List<Patient> patients = GetAll();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Jmbg.Equals(jmbg))
                {
                    patients[i].IsDeleted = true;
                    
                    return true;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(patients, Formatting.Indented);
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

        public String FileName { get; set; }

    }
}