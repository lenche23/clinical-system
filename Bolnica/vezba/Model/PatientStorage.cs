using System;
using System.Collections.Generic;

namespace Model
{
    public class PatientStorage
    {
        private List<Patient> patient = new List<Patient>();

        public PatientStorage()
        {
            this.FileName = "pacijenti.json";
            Allergen a1 = new Allergen("Ambrozija");
            Allergen a2 = new Allergen("BI-278");
            MedicalRecord mr = new MedicalRecord("07167166", "620178060616");
            mr.AddAllergen(a1);
            mr.AddAllergen(a2);

            Patient p1 = new Patient(false, "Mira", "Torbica", "1701994261473", new DateTime(1994, 1, 1), Sex.female, "0627844326", "Hajduk Veljkova 42, Novi Sad", "mira94@gmail.com", "008755974", "06699592141", mr, "miki17", "Tin3");
            Patient p2 = new Patient(false, "Petar", "Ilic", "1008985563244", new DateTime(1985, 8, 10), Sex.male, "0625564197", "Ulica Lipa 76, Beograd", "pilic@gmail.com", "007265981", "0638756221", null, "pera85", "p123");
            Patient p3 = new Patient(false, "Petar", "Ilic", "54114551", new DateTime(1985, 8, 10), Sex.male, "0625564197", "Ulica Lipa 76, Beograd", "pilic@gmail.com", "007265981", "0638756221", null, "pra85", "p123");
            p3.IsDeleted = true;
            p1.IsDeleted = false;
            patient.Add(p1);
            patient.Add(p2);
            patient.Add(p3);
        }
        public List<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();
            for (int i = 0; i < patient.Count; i++)
            {
                if(patient[i].IsDeleted == false)
                {
                    patients.Add(patient[i]);
                }
            }
            return patients;
            //throw new NotImplementedException();
        }

        public void Save(Patient p)
        {
            //samo upis direkt u fajl - dopisivanje
            patient.Add(p);
            //throw new NotImplementedException();
        }

        public Boolean Update(Patient p)
        {
            //dodati da se lista patients dobija iz get all metode
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
                    //upis u fajl svega
                    return true;
                }
            }
            return false;
            //throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }

        public Boolean Delete(string jmbg)
        {
            List<Patient> patients = GetAll();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Jmbg.Equals(jmbg))
                {
                    patients[i].IsDeleted = true;
                    //upis svega u fajl
                    return true;
                }
            }
            return false;
            //throw new NotImplementedException();
        }

        public String FileName { get; set; }

    }
}