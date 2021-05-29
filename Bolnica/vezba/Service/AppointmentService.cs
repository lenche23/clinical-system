using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using vezba;
using vezba.PatientPages;
using vezba.Repository;

namespace Service
{
   public class AppointmentService
    {
        public AppointmentFileRepository AppointmentRepository { get; }
        private Appointment ChangingAppointment { get; set; }
        private EventsLogService EventsLogService { get; set; }
        public AppointmentService()
        {
            AppointmentRepository = new AppointmentFileRepository();
            EventsLogService = new EventsLogService();
            ChangingAppointment = new Appointment();
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

        public List<Appointment> GetOverlapingAppointments(Appointment appointment)
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

        private DateTime FindNextFreeAppointmentStartTime(Appointment appointment)
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
        public Boolean MoveableAppointment(DateTime initDate, DateTime pickedDate)
        {
            int diff = (pickedDate - initDate).Days;
            if (diff > 2)
                return false;

            return true;
        }

        public void ChangeAppointment(Appointment initAppointment, DateTime pickedDate, String pickedTime)
        {
            int id = initAppointment.AppointentId;
            pickedDate.ToString("MM/dd/yyyy");
            Doctor initDoctor = initAppointment.Doctor;
            DateTime parsedTime = DateTime.ParseExact(pickedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime movedDate = pickedDate.Date.Add(parsedTime.TimeOfDay);

            Appointment changedAppointment = new Appointment(initDoctor, movedDate, PatientView.Patient);
            changedAppointment.AppointentId = id;
            EditAppointment(changedAppointment);

            Appointment idAppointment = ChangeAppointmentPage.Appointments.FirstOrDefault(a => a.AppointentId.Equals(id));
            if (idAppointment != null)
            {
                ChangeAppointmentPage.Appointments[ChangeAppointmentPage.Appointments.IndexOf(idAppointment)] = changedAppointment;
            }
        }

        public Boolean PatientCanScheduleAppointment(Appointment newAppointment)
        {
            if (GetOverlapingAppointments(newAppointment).Count == 0)
            {
                EventsLogService.AddLog();
                PatientNotification noti = new PatientNotification("Uspešno ste zakazali pregled.");
                noti.ShowDialog();
                return AppointmentRepository.Save(newAppointment);
            }
            else
            {
                PatientNotification noti = new PatientNotification("Termin je zauzet! Izaberite drugo vreme.");
                noti.ShowDialog();
                return false;
            }
        }

        public DateTime ParseTime(DateTime selectedDate, String selectedTime)
        {
            selectedDate.ToString("MM/dd/yyyy");
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);
            return dateTimeFinal;
        }

        public Boolean CanSchedule(DateTime selectedDate)
        {
            int diff = (selectedDate - DateTime.Now.Date).Days;
            if (diff <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Appointment> GetPatientFutureAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (appointment.StartTime.Date > DateTime.Today && appointment.Patient.Jmbg.Equals(PatientView.Patient.Jmbg))
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }
        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}
