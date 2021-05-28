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

        /*public Boolean SaveDoctor(Doctor newDoctor)
        {
            return DoctorRepository.Save(newDoctor);
        }

        public Boolean EditAppointment(Doctor editedDoctor)
        {
            return DoctorRepository.Update(editedDoctor);
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