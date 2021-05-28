using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
   public class PatientService
   {

        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************
        private PatientFileRepository FileRepository { get; set; }

        public PatientService()
        {
            FileRepository = new PatientFileRepository();
        }

        public List<Patient> GetAll()
        {
            return FileRepository.GetAll();
        }

        public Boolean Update(Patient updatedPatient)
        {
            return FileRepository.Update(updatedPatient);
        }

        public void AddPrescriptionToPatient(Patient patient, Prescription newPrescription)
        {
            patient.MedicalRecord.AddPrescription(newPrescription);
            Update(patient);
        }

        public void AddAnamnesisToPatient(Patient patient, Anamnesis newAnamnesis)
        {
            patient.MedicalRecord.AddAnamnesis(newAnamnesis);
            Update(patient);
        }

        public void AddReferralLetterToPatient(Patient patient, ReferralLetter newReferralLetter)
        {
            patient.MedicalRecord.AddReferralLetter(newReferralLetter);
            Update(patient);
        }

        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}