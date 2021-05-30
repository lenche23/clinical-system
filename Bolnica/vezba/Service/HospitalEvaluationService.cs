using Model;
using System;
using vezba.Repository;

namespace Service
{
   public class HospitalEvaluationService
   {
        private HospitalEvaluationFileRepository HospitalEvaluationRepository { get; set; }
        public HospitalEvaluationService()
        {
            HospitalEvaluationRepository = new HospitalEvaluationFileRepository();
        }
        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public int EvaluationGenerateNextId()
        {
            return HospitalEvaluationRepository.GenerateNextId();
        }

        public Boolean SaveEvaluation(HospitalEvaluation newEvaluation)
        {
            return HospitalEvaluationRepository.Save(newEvaluation);
        }




        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
        }
}