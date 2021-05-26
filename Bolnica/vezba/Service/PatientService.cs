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
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}