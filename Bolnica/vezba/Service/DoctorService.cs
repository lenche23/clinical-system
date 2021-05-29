using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Model;
using vezba.Repository;

namespace Service
{
   public class DoctorService
   {

       // Sekretar*******************************************************************************





       // SekretarKraj***************************************************************************

       // Pacijent*******************************************************************************





       // PacijentKraj***************************************************************************

       // Lekar**********************************************************************************
       private DoctorFileRepository DoctorRepository { get; }
       public DoctorService()
       {
           DoctorRepository = new DoctorFileRepository();
       }

       public List<Doctor> GetAllDoctors()
       {
           return DoctorRepository.GetAll();
       }
       // LekarKraj******************************************************************************

       // Upravnik*******************************************************************************





       // UpravnikKraj***************************************************************************
    }
}