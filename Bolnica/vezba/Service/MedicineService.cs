using System;
using System.Collections.Generic;
using Model;
using Model;
using vezba.Repository;
using vezba.Service;

namespace Service
{
    public class MedicineService
    {

        private IMedicineRepository MedicineRepository { get; }
        public DeclinedMedicineFileRepository DeclinedMedicineRepository { get; }

        public MedicineService(IMedicineRepository medicineRepository, IDeclinedMedicineRepository declinedMedicineRepository)
        {
            MedicineRepository = medicineRepository;
            DeclinedMedicineRepository = declinedMedicineRepository;
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
            var allApprovedMedicine = GetApproved();
            var validMedicineGenerator = new ValidMedicineGenerator(allApprovedMedicine, medicalRecord);
            return validMedicineGenerator.GenerateValidMedicineForPatient();
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
