﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Template
{
    class SearchPatients : Search<Patient>
    {
        protected override List<Patient> GetAll()
        {
            PatientService patientService = new PatientService();
            List<Patient> allPatients = patientService.GetAllPatients();
            return allPatients;
        }

        protected override bool ItemContainsInput(Patient patient, string input)
        {
            Boolean flag = false;
            if (patient.NameAndSurname.ToLower().Contains(input.ToLower()))
                flag = true;
            if (patient.Jmbg.ToLower().Contains(input.ToLower()))
                flag = true;
            return flag;
        }
    }
}
