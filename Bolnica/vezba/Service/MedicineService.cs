using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
   public class MedicineService
   {

        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************
        private MedicineFileRepository FileRepository { get; set; }

        public MedicineService()
        {
            FileRepository = new MedicineFileRepository();
        }

        public List<Medicine> GetApproved()
        {
            return FileRepository.GetApproved();
        }

        public List<Medicine> GetAwaiting()
        {
            return FileRepository.GetAwaiting();
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

        public Boolean Update(Medicine updatedMedicine)
        {
            return FileRepository.Update(updatedMedicine);
        }

        public Boolean Delete(int medicineID)
        {
            return FileRepository.Delete(medicineID);
        }

        public List<Medicine> GenerateValidMedicineForPatient(MedicalRecord medicalRecord)
        {
            List<Medicine>validMedicine = new List<Medicine>();
            foreach (var medicine in FileRepository.GetApproved())
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
            Update(medicineToApprove);
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}