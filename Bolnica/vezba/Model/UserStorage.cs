using System;
using System.Collections.Generic;

namespace Model
{
    public class UserStorage
    {
        public List<User> users = new List<User>();
        public UserStorage()
        {
            Doctor doctor = new Doctor { Name = "Djura", Surname = "Djuric", Jmbg = "1410999855161"};
            users.Add(doctor);
        }

        public List<User> GetAll()
        {
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