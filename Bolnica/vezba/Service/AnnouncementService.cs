using System;
using System.Collections.Generic;
using Model;
using vezba.Repository;

namespace Service
{
    public class AnnouncementService
    {
        public AnnouncementFileRepository AnnouncementRepository { get; }

        public AnnouncementService()
        {
            AnnouncementRepository = new AnnouncementFileRepository();
        }

        // Sekretar*******************************************************************************





        // SekretarKraj***************************************************************************

        // Pacijent*******************************************************************************





        // PacijentKraj***************************************************************************

        // Lekar**********************************************************************************





        // LekarKraj******************************************************************************

        // Upravnik*******************************************************************************

        public List<Announcement> GetAllManagerAnnouncements()
        {
            return AnnouncementRepository.GetAll();
        }

        public Boolean SaveManagerAnnouncement(Announcement newAnnouncement)
        {
            return AnnouncementRepository.Save(newAnnouncement);
        }

        public Boolean EditManagerAnnouncement(Announcement editedAnnouncement)
        {
            return AnnouncementRepository.Update(editedAnnouncement);
        }

        public Boolean DeleteManagerAnnouncement(int announcementId)
        {
            return AnnouncementRepository.Delete(announcementId);
        }

        public List<Announcement> GetManagerAnnouncementsByUser(UserType userType)
        {
            return AnnouncementRepository.GetByUser(userType);
        }

        // UpravnikKraj***************************************************************************
    }
}