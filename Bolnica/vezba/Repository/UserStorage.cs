// File:    UserFileRepository.cs
// Author:  bogdana
// Created: četvrtak, 25. mart 2021. 19.13.50
// Purpose: Definition of Class UserFileRepository

using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
   public class UserStorage1
   {
      public List<User> GetAll()
      {
         throw new NotImplementedException();
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
      
      public Boolean Delete(String username)
      {
         throw new NotImplementedException();
      }
      
      public String fileName;
   
   }
}