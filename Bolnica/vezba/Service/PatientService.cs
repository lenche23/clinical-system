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
        private PatientFileRepository PatientRepository { get; }

        public PatientService()
        {
            PatientRepository = new PatientFileRepository();
        }

        public List<Patient> GetAllPatients()
        {
            return PatientRepository.GetAll();
        }

        public Boolean UpdatePatient(Patient updatedPatient)
        {
            return PatientRepository.Update(updatedPatient);
        }

        public void AddPrescriptionToPatient(Patient patient, Prescription newPrescription)
        {
            patient.MedicalRecord.AddPrescription(newPrescription);
            UpdatePatient(patient);
        }

        public void AddAnamnesisToPatient(Patient patient, Anamnesis newAnamnesis)
        {
            patient.MedicalRecord.AddAnamnesis(newAnamnesis);
            UpdatePatient(patient);
        }

        public void AddReferralLetterToPatient(Patient patient, ReferralLetter newReferralLetter)
        {
            patient.MedicalRecord.AddReferralLetter(newReferralLetter);
            UpdatePatient(patient);
        }

        public void AddHospitalTreatmentToPatient(Patient patient, HospitalTreatment newHospitalTreatment)
        {
            patient.MedicalRecord.AddHospitalTreatment(newHospitalTreatment);
            UpdatePatient(patient);
        }

        public void ExtendHospitalTreatment()
        {

        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}