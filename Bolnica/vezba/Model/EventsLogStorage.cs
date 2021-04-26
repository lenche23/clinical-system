using System;
using System.Collections.Generic;

namespace Model
{
   public class EventsLogStorage
   {
      public List<EventsLog> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean Save(EventsLog log)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Update(EventsLog log)
      {
         throw new NotImplementedException();
      }
      
      public EventsLog GetOne(String patientJmbg)
      {
         throw new NotImplementedException();
      }
      
      public Boolean Delete(String patientJmbg)
      {
         throw new NotImplementedException();
      }
      
      public List<EventsLog> Load()
      {
         throw new NotImplementedException();
      }
      
      public String FileName { get; set; }

    }
}