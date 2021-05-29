using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Model;
using vezba;
using vezba.Repository;

namespace Service
{
   public class AppointmentService
   {
       private AppointmentFileRepository AppointmentRepository { get; }

       public AppointmentService()
       {
           AppointmentRepository = new AppointmentFileRepository();
       }
        // Sekretar*******************************************************************************

        public Appointment GetAppointmentById(int id)
        {
            return AppointmentRepository.GetOne(id);
        }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentRepository.GetAll();
        }

        public Boolean SaveAppointment(Appointment newAppointment)
        {
            return AppointmentRepository.Save(newAppointment);
        }

        public Boolean EditAppointment(Appointment editedAppointment)
        {
            return AppointmentRepository.Update(editedAppointment);
        }

        public Boolean ScheduleAppointment(Appointment newAppointment)
        {
            if (GetOverlapingAppointments(newAppointment).Count == 0)
            {
                return AppointmentRepository.Save(newAppointment);
            }
            else
            {
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.\n Prvi sledeci dostupan termin za unete kriterijume je " + FindNextFreeAppointmentStartTime(newAppointment).ToString("dd.MM.yyyy. HH:mm"));
                return false;
            }
        }

        public Boolean RescheduleAppointment(Appointment editedAppointment)
        {
            if (GetOverlapingAppointments(editedAppointment).Count == 0)
                return AppointmentRepository.Update(editedAppointment);
            else
            {
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.\nPrvi sledeci dostupan termin za unete kriterijume je " + FindNextFreeAppointmentStartTime(editedAppointment).ToString("dd.MM.yyyy. HH:mm"));
                return false;
            }
        }

        public Boolean DeleteAppointment(int id)
        {
            return AppointmentRepository.Delete(id);
        }

        private List<Appointment> GetOverlapingAppointments(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            List<Appointment> overlapingAppointments = new List<Appointment>();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (this.AppointmentsOverlap(appointment, scheduledAppointments[i]))
                {
                    overlapingAppointments.Add(scheduledAppointments[i]);
                }
            }
            return overlapingAppointments;
        }

        private Boolean AppointmentsOverlap(Appointment appointment1, Appointment appointment2)
        {
            if (AppointmentsShareDoctor(appointment1, appointment2) || AppointmentsSharePatient(appointment1, appointment2) || AppointmentsShareRoom(appointment1, appointment2))
            {
                if (DateTime.Compare(appointment2.EndTime, appointment1.StartTime) <= 0) //drugi zavrsava pre pocetka prvog
                    return false;
                else if (DateTime.Compare(appointment1.EndTime, appointment2.StartTime) <= 0) //prvi zavrsava pre pocetka drugog
                    return false;
                else
                    return true;
            }
            return false;
        }

        private Boolean AppointmentsSharePatient(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Patient.Jmbg.Equals(appointment2.Patient.Jmbg);
        }

        private Boolean AppointmentsShareDoctor(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Doctor.Jmbg.Equals(appointment2.Doctor.Jmbg);
        }

        private Boolean AppointmentsShareDoctorSpeciality(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Doctor.Speciality.Name.Equals(appointment2.Doctor.Speciality.Name);
        }

        private Boolean AppointmentsShareRoom(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.Room.RoomNumber == appointment2.Room.RoomNumber;
        }

        public DateTime FindNextFreeAppointmentStartTime(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (AppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }

        private DateTime CalculateNewStartTime(DateTime overlapingAppointmentEndTime)
        {
            DateTime newStartTime = overlapingAppointmentEndTime;
            if (IsAfterWorkingHours(newStartTime))
            {
                newStartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, 8, 0, 0);
                newStartTime = newStartTime.AddDays(1);
            }
            return newStartTime;
        }

        private Boolean IsAfterWorkingHours(DateTime time)
        {
            DateTime endOfDay = new DateTime(time.Year, time.Month, time.Day, 19, 45, 0);
            if (time >= endOfDay)
                return true;

            return false;
        }
        public Boolean ScheduleEmergencyAppointment(Appointment emergencyAppointment)
        {
            if (emergencyAppointment == null)
                return false;

            if (emergencyAppointment.StartTime <= DateTime.Now.AddMinutes(15))
            {
                AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
                return appointmentFileRepository.Save(emergencyAppointment);
            }
            return false;
        }
        public Appointment FindEarliestEmergencyAppointment(Appointment modelAppointment, Speciality speciality)
        {
            DoctorFileRepository doctorFileRepository = new DoctorFileRepository();
            List<Doctor> doctors = doctorFileRepository.GetDoctorsWithSpeciality(speciality);

            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in doctors)
            {
                Appointment emergencyAppointment = new Appointment(0, modelAppointment.Patient, d, modelAppointment.Room, DateTime.Now, modelAppointment.DurationInMunutes, modelAppointment.ApointmentDescription, true);

                emergencyAppointment.StartTime = FindNextFreeAppointmentStartTime(emergencyAppointment);
                appointments.Add(emergencyAppointment);
            }
            return FindEarliestOfAppointments(appointments);
        }

        private Appointment FindEarliestOfAppointments(List<Appointment> appointments)
        {
            if (appointments.Count == 0)
                return null;
            Appointment earliestAppoinment = appointments[0];
            foreach (Appointment a in appointments)
            {
                if (a.StartTime < earliestAppoinment.StartTime)
                    earliestAppoinment = a;
            }
            return earliestAppoinment;
        }
        //******************************************
        private List<Appointment> SortAppointmentsByStartTime(List<Appointment> appointments)
        {
            appointments.Sort((a1, a2) => a1.StartTime.CompareTo(a2.StartTime));
            return appointments;
        }

        private List<Appointment> GetEmergencyOverlapingAppointments(Appointment appointment)
        {
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            List<Appointment> overlapingAppointments = new List<Appointment>();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (this.EmergencyAppointmentsOverlap(appointment, scheduledAppointments[i]))
                {
                    overlapingAppointments.Add(scheduledAppointments[i]);
                }
            }
            return overlapingAppointments;
        }

        private DateTime FindNextFreeAppointmentStartTimeInAppointments(Appointment appointment, List<Appointment> scheduledAppointments)
        {
            Boolean newTimeFound = false;
            while (!newTimeFound)
            {
                newTimeFound = true;
                foreach (Appointment a in scheduledAppointments)
                {
                    if (EmergencyAppointmentsOverlap(a, appointment))
                    {
                        appointment.StartTime = CalculateNewStartTime(a.EndTime);
                        newTimeFound = false;
                        break;
                    }
                }
            }
            return appointment.StartTime;
        }
        private Boolean EmergencyAppointmentsOverlap(Appointment appointment1, Appointment appointment2)
        {
            if (AppointmentsShareDoctorSpeciality(appointment1, appointment2) || AppointmentsSharePatient(appointment1, appointment2) || AppointmentsShareRoom(appointment1, appointment2))
            {
                if (DateTime.Compare(appointment2.EndTime, appointment1.StartTime) <= 0) //drugi zavrsava pre pocetka prvog
                    return false;
                else if (DateTime.Compare(appointment1.EndTime, appointment2.StartTime) <= 0) //prvi zavrsava pre pocetka drugog
                    return false;
                else
                    return true;
            }
            return false;

        }
        public List<AppointmentForReschedulingDTO> CreateAppointmentsForRescheduling(Appointment emergencyAppointment)
        {
            List<Appointment> overlapingAppointments = GetEmergencyOverlapingAppointments(emergencyAppointment);
            AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
            List<Appointment> scheduledAppointments = appointmentFileRepository.GetAll();
            scheduledAppointments.Add(emergencyAppointment);
            RemoveAppointmentsFromAppointentList(overlapingAppointments, scheduledAppointments);
            overlapingAppointments = SortAppointmentsByStartTime(overlapingAppointments);
            return TransformAppointmentsInAppointmentsForRescheduling(overlapingAppointments, scheduledAppointments);

        }
        private List<AppointmentForReschedulingDTO> TransformAppointmentsInAppointmentsForRescheduling(List<Appointment> overlapingAppointments, List<Appointment> scheduledAppointments)
        {
            List<AppointmentForReschedulingDTO> appointmentsForRescheduling = new List<AppointmentForReschedulingDTO>();
            foreach (Appointment appointment in overlapingAppointments)
            {
                AppointmentForReschedulingDTO ad = new AppointmentForReschedulingDTO(appointment);
                ad.SuggestedTime = FindNextFreeAppointmentStartTimeInAppointments(appointment, scheduledAppointments);
                appointment.StartTime = ad.SuggestedTime;
                scheduledAppointments.Add(appointment);
                appointmentsForRescheduling.Add(ad);
            }

            return appointmentsForRescheduling;
        }

        private void RemoveAppointmentsFromAppointentList(List<Appointment> appointmentsForRemoval, List<Appointment> appointments)
        {
            foreach (Appointment oAppointment in appointmentsForRemoval)
            {
                var appointment = appointments.FirstOrDefault(a => a.AppointentId.Equals(oAppointment.AppointentId));
                if (appointment != null)
                    appointments.Remove(appointment);
            }
        }


        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************
        public Boolean DoctorRescheduleAppointment(Appointment editedAppointment)
        {
            List<Appointment> overlappingAppointments = GetOverlapingAppointments(editedAppointment);
            RemoveAppointment(editedAppointment, overlappingAppointments);
            if (overlappingAppointments.Count == 0)
                return AppointmentRepository.Update(editedAppointment);
            else
            {
                MessageBox.Show("Termin je zauzet! Izaberite drugo vreme.\nPrvi sledeci dostupan termin za unete kriterijume je " + FindNextFreeAppointmentStartTime(editedAppointment).ToString("dd.MM.yyyy. HH:mm"));
                return false;
            }
        }

        private static void RemoveAppointment(Appointment appointmentToRemove, List<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                if (appointment.AppointentId == appointmentToRemove.AppointentId)
                {
                    appointments.Remove(appointment);
                    break;
                }
            }
        }

        public TimeSpan GetEarliestTime(List<Appointment> appointments)
        {
            var earliestTime = appointments[0].StartTime.TimeOfDay;
            foreach (var appointment in appointments)
            {
                if (TimeSpan.Compare(appointment.StartTime.TimeOfDay, earliestTime) < 0)
                {
                    earliestTime = appointment.StartTime.TimeOfDay;
                }
            }

            return earliestTime;
        }

        public List<Appointment>GenerateAppointmentsForWeekAndDoctor(DateTime startOfWeek, DateTime endOfWeek, Doctor selectedDoctor)
        {
            List<Appointment> appointments = new List<Appointment>();
            List<Appointment> allAppointments = GetAllAppointments();

            foreach (var appointment in allAppointments)
            {
                if (appointment.Doctor == null)
                    continue;
                if(IsAppointmentInCurrentView(appointment, startOfWeek, endOfWeek, selectedDoctor))
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public Boolean IsAppointmentInCurrentView(Appointment appointment, DateTime startOfWeek, DateTime endOfWeek, Doctor selectedDoctor)
        {
            return DateTime.Compare(appointment.StartTime, startOfWeek) > 0 &&
                   DateTime.Compare(appointment.StartTime, endOfWeek) < 0 &&
                   appointment.Doctor.Jmbg.Equals(selectedDoctor.Jmbg);
        }
        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}