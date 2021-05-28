using System;
using System.Collections.Generic;
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

       public List<Appointment> GetAll()
       {
           return FileRepository.GetAll();
       }

       public void AddPrescriptionToAppointments(Patient patient, Prescription newPrescription)
       {
           List<Appointment> allAppointments = GetAll();
           foreach (var appointment in allAppointments)
           {
               if (!appointment.Patient.Jmbg.Equals(patient.Jmbg)) continue;

               appointment.Patient.MedicalRecord.AddPrescription(newPrescription);
               Update(appointment);
           }
       }

       public void AddAnamnesisToAppointments(Patient patient, Anamnesis newAnamnesis)
       {
           List<Appointment> allAppointments = GetAll();
           foreach (Appointment appointment in allAppointments)
           {
               if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
               {
                   appointment.Patient.MedicalRecord.AddAnamnesis(newAnamnesis);
                   Update(appointment);
               }
           }
       }

       public void AddReferralLetterToAppointments(Patient patient, ReferralLetter newReferralLetter)
       {
           List<Appointment> allAppointments = GetAll();
           foreach (Appointment appointment in allAppointments)
           {
               if (appointment.Patient.Jmbg.Equals(patient.Jmbg))
               {
                   appointment.Patient.MedicalRecord.AddReferralLetter(newReferralLetter);
                   Update(appointment);
               }
           }
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}