using System;
using System.Collections.Generic;

namespace Model
{
    public class UserStorage
    {
        public List<User> users = new List<User>();
        public UserStorage()
        {
            
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            Doctor doctor = new Doctor("Milica", "Milicevic", "2205972659017", DateTime.Now, Sex.female, "06364789110", "neka ulica 98, Novi Sad", "milica@gmail.com", "008100252", 100000, null, "milica", "mica");
            users.Add(doctor);
            return users; 
           //throw new NotImplementedException();
        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }

        public Boolean Update(User user)
        {
            throw new NotImplementedException();
        }

        public User GetOne(String username)
        {
            throw new NotImplementedException();
        }

        public String FileName { get; set; }

    }
}