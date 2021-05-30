using Model;
using System;
using System.Collections.Generic;
using vezba;
using vezba.Repository;

namespace Service
{
   public class EventsLogService
   {
        public EventsLogFileRepository EventsLogRepository { get; set; }
        public EventsLogService()
        {
            EventsLogRepository = new EventsLogFileRepository();
        }
        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************
        public List<EventsLog> LoadEvents()
        {
           return EventsLogRepository.ReadFromFile();
        }

        private Boolean Update(EventsLog log)
        {
            return EventsLogRepository.Update(log);
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

        public List<EventsLog> GetAllLogs()
        {
            return EventsLogRepository.ReadFromFile();
        }

        public Boolean EditLog(EventsLog log)
        {
            return EventsLogRepository.Update(log);
        }
        
            // PacijentKraj***************************************************************************

            // Lekar**********************************************************************************





            // LekarKraj******************************************************************************

            // Upravnik*******************************************************************************





            // UpravnikKraj***************************************************************************
        }
}