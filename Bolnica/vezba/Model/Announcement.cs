// File:    Announcement.cs
// Author:  graho
// Created: 15 April 2021 20:26:10
// Purpose: Definition of Class Announcement

using System;

namespace Model
{
   public class Announcement
   {
        public Announcement(int id, DateTime po, DateTime ed, string tit, string con, Visibility vis)
        {
            Id = id;
            Posted = po;
            Edited = ed;
            Title = tit;
            Content = con;
            Visibility = vis;
        }

        public int Id { get; set; }
        public DateTime Posted { get; set; }
        public DateTime Edited { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public Visibility Visibility { get; set; }
    }
}