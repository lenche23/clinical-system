using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
   public class RoomService
   {

       // Sekretar*******************************************************************************





       // SekretarKraj***************************************************************************

       // Pacijent*******************************************************************************





       // PacijentKraj***************************************************************************

       // Lekar**********************************************************************************
       private RoomFileRepository FileRepository { get; set; }

       public RoomService()
       {
           FileRepository = new RoomFileRepository();
       }

       public List<Room> GetAll()
       {
           return FileRepository.GetAll();
       }
       // LekarKraj******************************************************************************

       // Upravnik*******************************************************************************





       // UpravnikKraj***************************************************************************
    }
}