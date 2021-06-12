using System;
using System.Collections.Generic;
using Model;
using Model;
using vezba.Repository;

namespace Service
{
    public class MedicineService
    {

        private IMedicineRepository MedicineRepository { get; }
        public DeclinedMedicineFileRepository DeclinedMedicineRepository { get; }

        public MedicineService()
        {
            MedicineRepository = new MedicineFileRepository();
            DeclinedMedicineRepository = new DeclinedMedicineFileRepository();
        }


        // Sekretar*******************************************************************************


        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************


        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************


        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************

        public List<Medicine> GetApproved()
        {
            return MedicineRepository.GetApproved();
        }

        public List<Medicine> GetAwaiting()
        {
            return MedicineRepository.GetAwaiting();
        }

        public List<Medicine> GetPossibleReplacements(Medicine medicine)
        {
            List<Medicine> medicineForReplacement = GetApproved();
            foreach (var replacement in medicineForReplacement)
            {
                if (replacement.MedicineID == medicine.MedicineID)
                {
                    medicineForReplacement.Remove(replacement);
                    break;
                }
            }

            return medicineForReplacement;
        }

        public Boolean UpdateMedicine(Medicine updatedMedicine)
        {
            return MedicineRepository.Update(updatedMedicine);
        }

        public Boolean DeleteMedicine(int medicineID)
        {
            return MedicineRepository.Delete(medicineID);
        }

        public List<Medicine> GenerateValidMedicineForPatient(MedicalRecord medicalRecord)
        {
            List<Medicine>validMedicine = new List<Medicine>();
            foreach (var medicine in MedicineRepository.GetApproved())
            {
                if (!AllergenMatchFound(medicine, medicalRecord))
                    validMedicine.Add(medicine);
            }

            return validMedicine;
        }

        private bool AllergenMatchFound(Medicine medicine, MedicalRecord medicalRecord)
        {
            var allergenMatchFound = false;
            foreach (var ingredient in medicine.ingridient)
            {
                foreach (var allergen in medicalRecord.Allergen)
                {
                    if (ingredient.Name.Equals(allergen.Name))
                    {
                        allergenMatchFound = true;
                    }
                }
            }

            return allergenMatchFound;
        }

        public void ApproveMedicine(Medicine medicineToApprove)
        {
            medicineToApprove.Status = MedicineStatus.approved;
            UpdateMedicine(medicineToApprove);
        }

        public List<DeclinedMedicine> GetDeclined()
        {
            return DeclinedMedicineRepository.GetAll();
        }

        public Boolean SaveDeclinedMedicine(DeclinedMedicine declinedMedicine)
        {
            declinedMedicine.DeclinedMedicineID = DeclinedMedicineRepository.GenerateNextId();
            return DeclinedMedicineRepository.Save(declinedMedicine);
        }

        public DeclinedMedicine DeclineMedicine(Medicine medicineToDecline, String description)
        {
            var declinedMedicine = new DeclinedMedicine(0, medicineToDecline, description);
            DeleteMedicine(medicineToDecline.MedicineID);
            SaveDeclinedMedicine(declinedMedicine);
            return declinedMedicine;
        }

        public Medicine getMedicineById(int medicineId) 
        {
            return MedicineRepository.GetOne(medicineId);        
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************
        public List<Medicine> GetAllMedicine()
        {
            return MedicineRepository.GetAll();
        }

        public Boolean SaveMedicine(Medicine newMedicine)
        {
            return MedicineRepository.Save(newMedicine);
        }

        public List<DeclinedMedicine> GetAllDeclinedMedicine()
        {
            return DeclinedMedicineRepository.GetAll();
        }

        public Boolean DeleteDeclinedMedicine(int medicineId)
        {
            return DeclinedMedicineRepository.Delete(medicineId);
        }

        // UpravnikKraj***************************************************************************
    }
}
