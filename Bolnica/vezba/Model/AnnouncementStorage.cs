using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Model
{
   public class AnnouncementStorage
   {
      public AnnouncementStorage()
      {
          this.FileName = "../../obavestenja.json";
      }
        public List<Announcement> GetAll()
        {
            List<Announcement> ans = this.Load();
            List<Announcement> announcements = new List<Announcement>();
            for (int i = 0; i < ans.Count; i++)
            {
                if (ans[i].IsDeleted == false)
                {
                    announcements.Add(ans[i]);
                }
            }
            return announcements;
        }

      public Boolean Save(Announcement a)
      {
            List<Announcement> announcements = Load();

            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == a.Id)
                {
                    return false;
                }
            }
            announcements.Add(a);
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(announcements, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e)
            {

            }
            return true;
        }
      
      public Boolean Update(Announcement a)
      {
            List<Announcement> announcements = Load();
            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == a.Id && announcements[i].IsDeleted == false)
                {
                    announcements[i].Edited = a.Edited;
                    announcements[i].Title = a.Title;
                    announcements[i].Content = a.Content;
                    announcements[i].Visibility = a.Visibility;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(announcements, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return false;
        }
      
      public Announcement GetOne(int id)
      {
            List<Announcement> announcements = GetAll();
            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == id && announcements[i].IsDeleted == false)
                {
                    return announcements[i];
                }
            }
            return null;
        }
      
      public Boolean Delete(int id)
      {
            List<Announcement> announcements = Load();
            for (int i = 0; i < announcements.Count; i++)
            {
                if (announcements[i].Id == id && announcements[i].IsDeleted == false)
                {
                    announcements[i].IsDeleted = true;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(announcements, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return true;

                }
            }
            return false;
        }
      
      public List<Announcement> Load()
      {
            List<Announcement> ans = new List<Announcement>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<Announcement> announcements = JsonConvert.DeserializeObject<List<Announcement>>(jsonFromFile);

                return announcements;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return ans;
      }

       public List<Announcement> GetByUser(UserType ut)
        {

            List<Announcement> temp = GetAll();
            List<Announcement> announcements = new List<Announcement>();
            if (ut == UserType.secretary || ut == UserType.menager || ut == UserType.doctor)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].Visibility == Model.Visibility.staff || temp[i].Visibility == Model.Visibility.all)
                    {
                        announcements.Add(temp[i]);
                    }
                }
            }
            else if (ut == UserType.patient)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].Visibility == Model.Visibility.patients || temp[i].Visibility == Model.Visibility.all)
                    {
                        announcements.Add(temp[i]);
                    }
                }
            }
            return announcements;
        }
        public int generateNextId()
        {
            List<Announcement> list = Load();
            return list.Count;
        }

        public String FileName { get; set; }
   
   }
}