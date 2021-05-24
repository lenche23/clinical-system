using Model;
using System;
using System.Collections.Generic;
using vezba;
using vezba.Repository;

namespace Service
{
   public class EventsLogService
   {
        public EventsLogFileRepository repository = new EventsLogFileRepository();
        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public List<EventsLog> LoadEvents()
        {
           return repository.Load();
        }

        private Boolean Update(EventsLog log)
        {
            return repository.Update(log);
        }

        public void AddLog()
        {
            List<EventsLog> list = LoadEvents();
            String patientJMBG = PatientView.Patient.Jmbg;
            DateTime log = DateTime.Now;
            foreach (EventsLog elog in list)
            {
                if (elog.PatientJmbg.Equals(patientJMBG))
                {
                    elog.EventDates.Add(log);
                    this.Update(elog);
                }
            }
        }


        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************





        // UpravnikKraj***************************************************************************
    }
}