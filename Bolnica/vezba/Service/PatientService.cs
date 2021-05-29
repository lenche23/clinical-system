using Model;
using System;
using System.Collections.Generic;
using vezba.Repository;

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





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }

}