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

        public void AddLog()
        {
            List<EventsLog> list = GetAllLogs();
            String patientJMBG = PatientView.Patient.Jmbg;
            DateTime log = DateTime.Now;
            foreach (EventsLog elog in list)
            {
                if (elog.PatientJmbg.Equals(patientJMBG))
                {
                    elog.EventDates.Add(log);
                    this.EditLog(elog);
                }
            }
        }

        public List<EventsLog> GetAllLogs()
        {
            return EventsLogRepository.GetAll();
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