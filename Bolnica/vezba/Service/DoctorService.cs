using Model;
using System;
using System.Collections.Generic;
using vezba.Repository;

namespace Service
{
   public class DoctorService
    {
        public DoctorFileRepository DoctorRepository { get; }

        public DoctorService()
        {
            DoctorRepository = new DoctorFileRepository();
        }
        // Sekretar*******************************************************************************
        public Doctor GetDoctorByJmbg(string jmbg)
        {
            return DoctorRepository.GetOne(jmbg);
        }

        public List<Doctor> GetAllDoctors()
        {
            return DoctorRepository.GetAll();
        }

        public List<Doctor> GetDoctorsWithSpeciality(Speciality speciality)
        {
            return DoctorRepository.GetDoctorsWithSpeciality(speciality);
        }

        public Boolean EditDoctor(Doctor editedDoctor)
        {
            Doctor doctor = SortWorkingHoursForDoctor(editedDoctor);
            return DoctorRepository.Update(doctor);
        }

        public List<WorkingHours> GetFutureWorkingHoursForDoctor(string jmbg)
        {
            List<WorkingHours> futureWorkingHours = new List<WorkingHours>();
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            //Doctor doctor = SortWorkingHoursForDoctor(DoctorRepository.GetOne(jmbg));
            foreach (WorkingHours workingHours in doctor.WorkingSchedule)
            {
                if (workingHours.EndDate > DateTime.Now)
                    futureWorkingHours.Add(workingHours);
            }
            return futureWorkingHours;
        }

        public Doctor SortWorkingHoursForDoctor(Doctor doctor)
        {
            doctor.WorkingSchedule.Sort((wh1, wh2) => wh1.BeginningDate.CompareTo(wh2.BeginningDate));
            return doctor;
        }

        public DateTime FindNextWorkingHoursBeginningDateForDoctor(string jmbg)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            DateTime nextBeginningDate = DateTime.Now;
            foreach (WorkingHours workingHours in doctor.WorkingSchedule)
            {
                if (workingHours.BeginningDate <= nextBeginningDate && workingHours.EndDate > nextBeginningDate)
                    nextBeginningDate = workingHours.EndDate;
            }
            return nextBeginningDate;
        }

        public void AddWorkingHoursToDoctor(string jmbg, WorkingHours workingHours)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            doctor.WorkingSchedule.Add(workingHours);
            EditDoctor(doctor);
        }

        public void RemoveWorkingHoursFromDoctor(string jmbg, WorkingHours selectedWorkingHours)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (WorkingHours wh in doctor.WorkingSchedule)
            {
                if (wh.BeginningDate.ToString("dd.MM.yyyy.") == selectedWorkingHours.BeginningDate.ToString("dd.MM.yyyy."))
                {
                    doctor.WorkingSchedule.Remove(wh);
                    break;
                }
            }
            EditDoctor(doctor);
        }




        /*public Boolean SaveDoctor(Doctor newDoctor)
        {
            return DoctorRepository.Save(newDoctor);
        }

        public Boolean DeleteDoctor(string jmbg)
        {
            return DoctorRepository.Delete(jmbg);
        }*/




        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}