using Model;
using System;
using System.Collections.Generic;
using System.Windows;
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
            //doctor = SortVacationDaysForDoctor(doctor);
            return DoctorRepository.Update(doctor);
        }

        public List<WorkingHours> GetFutureWorkingHoursForDoctor(string jmbg)
        {
            List<WorkingHours> futureWorkingHours = new List<WorkingHours>();
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (WorkingHours workingHours in doctor.WorkingSchedule)
            {
                if (workingHours.EndDate > DateTime.Now)
                    futureWorkingHours.Add(workingHours);
            }
            return futureWorkingHours;
        }

        public List<VacationDays> GetFutureVacationDaysForDoctor(string jmbg)
        {
            List<VacationDays> futureVacationDays = new List<VacationDays>();
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (VacationDays vacationDays in doctor.VacationDays)
            {
                if (vacationDays.EndDate > DateTime.Now)
                    futureVacationDays.Add(vacationDays);
            }
            return futureVacationDays;
        }

        public Doctor SortWorkingHoursForDoctor(Doctor doctor)
        {
            doctor.WorkingSchedule.Sort((wh1, wh2) => wh1.BeginningDate.CompareTo(wh2.BeginningDate));
            return doctor;
        }
        public Doctor SortVacationDaysForDoctor(Doctor doctor)
        {
            doctor.VacationDays.Sort((vd1, vd2) => vd1.StartDate.CompareTo(vd2.StartDate));
            return doctor;
        }

        public DateTime FindNextWorkingHoursBeginningDateForDoctor(string jmbg)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            DateTime nextStartDate = DateTime.Now;
            foreach (WorkingHours workingHours in doctor.WorkingSchedule)
            {
                if (workingHours.BeginningDate <= nextStartDate && workingHours.EndDate > nextStartDate)
                    nextStartDate = workingHours.EndDate;
            }
            return nextStartDate;
        }

        public void AddWorkingHoursToDoctor(string jmbg, WorkingHours workingHours)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            doctor.WorkingSchedule.Add(workingHours);
            EditDoctor(doctor);
        }

        public Boolean AddVacationDaysToDoctor(string jmbg, VacationDays newVacationDays)
        {
            if (!VacationDaysOverlap(jmbg, newVacationDays))
            {
                Doctor doctor = DoctorRepository.GetOne(jmbg);
                doctor.VacationDays.Add(newVacationDays);
                EditDoctor(doctor);
                return true;
            }
            return false;
        }

        public Boolean VacationDaysOverlap(string jmbg, VacationDays newVacationDays)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (VacationDays vd in doctor.VacationDays)
            {
                if (!(vd.EndDate < newVacationDays.StartDate || newVacationDays.EndDate < vd.StartDate))
                {
                    MessageBox.Show("Dolazi do preklapanja godisnjih odmora!");
                    return true;
                }
            }
            return false;
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
        public void RemoveVacationDaysFromDoctor(string jmbg, VacationDays selectedVacationDays)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (VacationDays vd in doctor.VacationDays)
            {
                if (vd.StartDate.ToString("dd.MM.yyyy.") == selectedVacationDays.StartDate.ToString("dd.MM.yyyy."))
                {
                    doctor.VacationDays.Remove(vd);
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