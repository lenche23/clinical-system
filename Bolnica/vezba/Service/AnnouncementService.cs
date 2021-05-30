using Model;
using System;
using System.Collections.Generic;
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

        public List<Announcement> GetAllAnnouncements()
        {
            return AnnouncementRepository.GetAll();
        }

        public Boolean SaveAnnouncement(Announcement newAnnouncement)
        {
            return AnnouncementRepository.Save(newAnnouncement);
        }

        public Boolean EditAnnouncement(Announcement editedAnnouncement)
        {
            return AnnouncementRepository.Update(editedAnnouncement);
        }

        public Boolean DeleteAnnouncement(int announcementId)
        {
            return AnnouncementRepository.Delete(announcementId);
        }

        public List<Announcement> GetAnnouncementsByUserType(UserType userType)
        {
            return AnnouncementRepository.GetByUserType(userType);
        }

        public List<Announcement> GetUserIndividualAnnouncements(String userId)
        {
            return AnnouncementRepository.GetIndividualAnnouncements(userId);
        }
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
