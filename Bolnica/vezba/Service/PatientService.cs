using Model;
using System;
using System.Collections.Generic;
using vezba;
using vezba.Repository;
using System.Threading;

namespace Service
{
   public class PatientService
    {
        public PatientFileRepository PatientRepository { get; }

        public PatientService()
        {
            PatientRepository = new PatientFileRepository();
        }
        // Sekretar*******************************************************************************
        public Patient GetPatientByJmbg(string jmbg)
        {
            return PatientRepository.GetOne(jmbg);
        }

        public List<Patient> GetAllPatients()
        {
            return PatientRepository.GetAll();
        }

        public Boolean SavePatient(Patient newPatient)
        {
            return PatientRepository.Save(newPatient);
        }

        public Boolean EditPatient(Patient editedPatient)
        {
            return PatientRepository.Update(editedPatient);
        }

        public Boolean DeletePatient(string jmbg)
        {
            return PatientRepository.Delete(jmbg);
        }

        public void UnblockPatient(string jmbg)
        {
            Patient blockedPatient = PatientRepository.GetOne(jmbg);
            blockedPatient.IsBlocked = false;
            PatientRepository.Update(blockedPatient);
        }

        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public Patient LoadPatient()
        {
            Patient patient = PatientRepository.GetOne("1008985563244");
            return patient;
        }

        public void AddHardcorePrescriptions()
        {
            var medicine1 = new Medicine("Vitamin C", "nesto", "nesto", 0, MedicineCondition.pill);
            Prescription p1 = new Prescription(DateTime.Now, 21, Period.daily, 1, 0, true, medicine1);
            var medicine2 = new Medicine("Brufen", "nesto", "nesto", 0, MedicineCondition.pill);
            Prescription p2 = new Prescription(DateTime.Now, 21, Period.daily, 2, 1, true, medicine2);
            MedicalRecord medicalRecord = new MedicalRecord("12345", "12345");
            PatientView.Patient.MedicalRecord = medicalRecord;
            PatientView.Patient.MedicalRecord.AddPrescription(p1);
            PatientView.Patient.MedicalRecord.AddPrescription(p2);
        }

        public void MedicineNotification()
        {
            while (true)
            {
                foreach (Prescription p in PatientView.Patient.MedicalRecord.prescription)
                {
                    GenerateNotification(p);
                }
                Thread.Sleep(TimeSpan.FromMinutes(5));
            }
        }

        public List<DateTime> GenerateNotification(Prescription prescription)
        {
            List<DateTime> notifications = AddTimeToSpan(prescription);
            foreach (DateTime dt in notifications)
            {
                if (CanShowNotification(dt))
                {
                    PatientNotification noti = new PatientNotification("Podsetnik da lek " + prescription.Medicine.Name + "trebate popiti u " + dt.ToString("HH:mm"));
                    noti.Show();
                }
            }
            return notifications;
        }

        private Boolean CanShowNotification(DateTime dateTime)
        {
            if (dateTime >= DateTime.Now && dateTime <= DateTime.Now.AddMinutes(5))
            {
                return true;
            }
            return false;
        }

        private List<DateTime> AddTimeToSpan(Prescription prescription)
        {
            DateTime it = new DateTime();
            it = prescription.StartDate.AddSeconds(0);
            DateTime start = prescription.StartDate;
            DateTime end = prescription.StartDate.AddDays(prescription.DurationInDays);
            List<DateTime> notifications = new List<DateTime>();

            while (it.Date < end.Date)
            {
                if (PeriodDaily(prescription))
                {
                    for (int i = 0; i < prescription.Number; i++)
                    {
                        notifications.Add(start.AddHours(i * 24 / prescription.Number));
                    }
                    it = it.AddDays(1);
                }
                else
                {
                    for (int i = 0; i < prescription.Number; i++)
                    {
                        notifications.Add(start.AddDays(i * 7 / prescription.Number));
                    }
                    it = it.AddDays(7);
                }
            }
            return notifications;
        }

        private Boolean PeriodDaily(Prescription p)
        {
            if (p.ReferencePeriod == Period.daily)
                return true;
            return false;
        }
        // PacijentKraj***************************************************************************
        // Lekar**********************************************************************************

        public void AddPrescriptionToPatient(Patient patient, Prescription newPrescription)
        {
            patient.MedicalRecord.AddPrescription(newPrescription);
            EditPatient(patient);
        }

        public void AddAnamnesisToPatient(Patient patient, Anamnesis newAnamnesis)
        {
            patient.MedicalRecord.AddAnamnesis(newAnamnesis);
            EditPatient(patient);
        }

        public void AddReferralLetterToPatient(Patient patient, ReferralLetter newReferralLetter)
        {
            patient.MedicalRecord.AddReferralLetter(newReferralLetter);
            EditPatient(patient);
        }

        public void AddHospitalTreatmentToPatient(Patient patient, HospitalTreatment newHospitalTreatment)
        {
            patient.MedicalRecord.AddHospitalTreatment(newHospitalTreatment);
            EditPatient(patient);
        }

        public void RemovePrescriptionFromPatient(Patient patient, Prescription prescription)
        {
            patient.MedicalRecord.RemovePrescription(prescription);
            EditPatient(patient);
        }

        public void RemoveReferralLetterFromPatient(Patient patient, ReferralLetter referralLetter)
        {
            patient.MedicalRecord.RemoveReferralLetter(referralLetter);
            EditPatient(patient);
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }

}
