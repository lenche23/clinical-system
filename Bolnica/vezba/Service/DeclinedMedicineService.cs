using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
   public class DeclinedMedicineService
   {
        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************
        private DeclinedMedicineFileRepository FileRepository { get; set; }

        public DeclinedMedicineService()
        {
            FileRepository = new DeclinedMedicineFileRepository();
        }

        public List<DeclinedMedicine> GetAll()
        {
            return FileRepository.GetAll();
        }

        public Boolean Save(DeclinedMedicine declinedMedicine)
        {
            declinedMedicine.DeclinedMedicineID = FileRepository.GenerateNextId();
            return FileRepository.Save(declinedMedicine);
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}