﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba.Template
{
    class SearchAppointments : Search<Appointment>
    {
        protected override List<Appointment> GetAll()
        {
            AppointmentService appointmentService = new AppointmentService();
            List<Appointment> allAppointments = appointmentService.GetAllAppointments();
            return allAppointments;
        }

        protected override bool ItemContainsInput(Appointment appointment, string input)
        {
            Boolean flag = false;
            if (appointment.DoctorName.ToLower().Contains(input.ToLower()))
                flag = true;
            if (appointment.PatientName.ToLower().Contains(input.ToLower()))
                flag = true;
            if (appointment.RoomName.ToLower().Contains(input.ToLower()))
                flag = true;
            if (appointment.DurationInMunutes.ToString().Contains(input))
                flag = true;
            return flag;
        }
    }
}
