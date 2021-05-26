using System;
using Model;
using vezba.Repository;

namespace Service
{
   public class AppointmentService
   {

       // Sekretar*******************************************************************************





       // SekretarKraj***************************************************************************

       // Pacijent*******************************************************************************





       // PacijentKraj***************************************************************************

       // Lekar**********************************************************************************
       private AppointmentFileRepository FileRepository { get; set; }

       public AppointmentService()
       {
           FileRepository = new AppointmentFileRepository();
       }

       public Boolean Save(Appointment newAppointment)
       {
           newAppointment.AppointentId = FileRepository.generateNextId();
           return FileRepository.Save(newAppointment);
       }

       public Boolean Update(Appointment updatedAppointment)
       {
           return FileRepository.Update(updatedAppointment);
       }

       public Boolean Delete(int appointmentID)
       {
           return FileRepository.Delete(appointmentID);
       }
       // LekarKraj******************************************************************************

       // Upravnik*******************************************************************************





       // UpravnikKraj***************************************************************************
    }
}