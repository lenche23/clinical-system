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
       private DoctorFileRepository FileRepository { get; set; }
       public DoctorService()
       {
           FileRepository = new DoctorFileRepository();
       }

       public List<Doctor> GetAll()
       {
           return FileRepository.GetAll();
       }
       // LekarKraj******************************************************************************

       // Upravnik*******************************************************************************





       // UpravnikKraj***************************************************************************
    }
}