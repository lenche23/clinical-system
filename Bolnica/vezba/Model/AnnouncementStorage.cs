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
            List<Announcement> allAnouncements = GetAll();
            List<Announcement> announcementsForUserType = new List<Announcement>();
            
            foreach(Announcement a in allAnouncements)
            {
                if (DoesUserTypeSeeAnnouncement(ut, a.Visibility))
                    announcementsForUserType.Add(a);
            }
            return announcementsForUserType;
        }

        public List<Announcement> getIndividualAnnouncements(String userId)
        {
            List<Announcement> allAnouncements = GetAll();
            List<Announcement> individualAnnouncements = new List<Announcement>();
            foreach(Announcement a in allAnouncements)
            {
                if(a.Visibility == Visibility.individual)
                {
                    foreach(String recipient in a.Recipients)
                    {
                        if (recipient.Equals(userId))
                            individualAnnouncements.Add(a);
                    }
                }
            }

            return individualAnnouncements;
        }
        
        private Boolean DoesUserTypeSeeAnnouncement(UserType userType, Model.Visibility visibility)
        {
            if (userType == UserType.menager && (visibility == Model.Visibility.all || visibility == Model.Visibility.staff || visibility == Model.Visibility.menagers))
                return true;
            else if (userType == UserType.secretary && (visibility == Model.Visibility.all || visibility == Model.Visibility.staff || visibility == Model.Visibility.secretaries))
                return true;
            else if (userType == UserType.doctor && (visibility == Model.Visibility.all || visibility == Model.Visibility.staff || visibility == Model.Visibility.doctors))
                return true;
            else if (userType == UserType.patient && (visibility == Model.Visibility.all || visibility == Model.Visibility.patients))
                return true;
            else 
                return false;
        }
        public int generateNextId()
        {
            List<Announcement> list = Load();
            return list.Count;
        }

        public String FileName { get; set; }
   
   }
}