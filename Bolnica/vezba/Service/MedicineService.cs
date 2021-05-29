using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
    public class MedicineService
    {

        public MedicineFileRepository MedicineRepository { get; }
        public DeclinedMedicineFileRepository DeclinedMedicineRepository { get; }

        public MedicineService()
        {
            MedicineRepository = new MedicineFileRepository();
            DeclinedMedicineRepository = new DeclinedMedicineFileRepository();
        }


        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





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

        public Boolean UpdateMedicine(Medicine updatedMedicine)
        {
            return MedicineRepository.Update(updatedMedicine);
        }

        public Boolean DeleteMedicine(int medicineId)
        {
            return MedicineRepository.Delete(medicineId);
        }

        public int GenerateNextMedicineId()
        {
            return MedicineRepository.GenerateNextId();
        }

        public List<DeclinedMedicine> GetAllDeclinedMedicine()
        {
            return DeclinedMedicineRepository.GetAll();
        }

        // UpravnikKraj***************************************************************************
    }
}