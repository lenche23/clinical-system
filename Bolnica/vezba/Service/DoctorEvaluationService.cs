using Model;
using System;
using System.Collections.Generic;
using vezba.Repository;

namespace Service
{
   public class DoctorEvaluationService
   {
        private DoctorEvaluationFileRepository DoctorEvaluationRepository { get; set; }
        public DoctorEvaluationService()
        {
            DoctorEvaluationRepository = new DoctorEvaluationFileRepository();
        }
        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************

        public Boolean SaveEvaluation(DoctorEvaluation newEvaluation)
        {
            return DoctorEvaluationRepository.Save(newEvaluation);
        }

        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}