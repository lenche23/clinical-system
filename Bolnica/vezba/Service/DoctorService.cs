using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using vezba.Repository;
using vezba.SecretaryGUI;

namespace Service
{
   public class DoctorService
    {
        private IDoctorRepository DoctorRepository { get; set; }

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
        //PREBACENO U TEMPLATE
        /*
        public List<Doctor> GetSearchResultDoctors(String search)
        {
            List<Doctor> allDoctors = GetAllDoctors();//DoctorRepository.GetAll();
            List<Doctor> doctors = new List<Doctor>();
            Boolean flag = false;
            foreach (Doctor d in allDoctors)
            {
                flag = false;
                if (d.NameAndSurname.ToLower().Contains(search.ToLower()))
                    flag = true;
                if (d.SpecialityName.ToLower().Contains(search.ToLower()))
                    flag = true;
                if (d.Jmbg.ToLower().Contains(search.ToLower()))
                    flag = true;
                if (flag == true)
                    doctors.Add(d);
            }
            return doctors;
        }*/

        public List<Doctor> GetDoctorsWithSpeciality(Speciality speciality)
        {
            return DoctorRepository.GetDoctorsWithSpeciality(speciality);
        }

        public Boolean EditDoctor(Doctor editedDoctor)
        {
            Doctor doctor = SortWorkingHoursForDoctor(editedDoctor);
            doctor = SortVacationDaysForDoctor(doctor);
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

        private Doctor SortWorkingHoursForDoctor(Doctor doctor)
        {
            doctor.WorkingSchedule.Sort((wh1, wh2) => wh1.BeginningDate.CompareTo(wh2.BeginningDate));
            return doctor;
        }
        private Doctor SortVacationDaysForDoctor(Doctor doctor)
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

        private Boolean VacationDaysOverlap(string jmbg, VacationDays newVacationDays)
        {
            Doctor doctor = DoctorRepository.GetOne(jmbg);
            foreach (VacationDays vd in doctor.VacationDays)
            {
                if (!(vd.EndDate < newVacationDays.StartDate || newVacationDays.EndDate < vd.StartDate))
                {
                    SecretaryMessage m1 = new SecretaryMessage("Dolazi do preklapanja godisnjih odmora!");
                    m1.ShowDialog();
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


        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public Doctor LoadDoctor()
        {
            return DoctorRepository.GetOne("1708962324890");
        }




        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}