using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
   public class EquipmentService
   {
        private IEquipmentRepository EquipmentRepository { get; }

        public EquipmentService()
        {
            EquipmentRepository = new EquipmentFileRepository();
        }


        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************

        public List<Equipment> GetAllEquipment()
        {
            return EquipmentRepository.GetAll();
        }
        
        public Boolean SaveEquipment(Equipment newEquipment)
        {
            return EquipmentRepository.Save(newEquipment);
        }

        public Boolean UpdateEquipment(Equipment updatedEquipment)
        {
            return EquipmentRepository.Update(updatedEquipment);
        }

        public Boolean DeleteEquipment(int equipmentId)
        {
            return EquipmentRepository.Delete(equipmentId);
        }
        // UpravnikKraj***************************************************************************
    }
}