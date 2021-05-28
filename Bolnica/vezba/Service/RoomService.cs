using Model;
using System;
using System.Collections.Generic;
using vezba.Repository;

namespace Service
{
   public class RoomService
    {
        public RoomFileRepository RoomRepository { get; }

        public RoomService()
        {
            RoomRepository = new RoomFileRepository();
        }
        // Sekretar*******************************************************************************

        public List<Room> GetAllRooms()
        {
            return RoomRepository.GetAll();
        }



        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }

}