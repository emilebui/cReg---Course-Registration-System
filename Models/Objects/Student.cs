﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cReg_WebApp.Models.Objects
{
    public class Student
    {
        public string name { get; }
        public int id { get; }
        public Faculty major { get; set; }
        public Faculty minor { get; set; }
        public int currYear { get; set; }

        public string password { get; set; }
        Shortlist shortlist = null;
        CompletedCourses completedCourses = null;

        public Student() { }


        public Student (string name, int id, int currYear,string password)
        {
            this.name = name;
            this.id = id;
            this.currYear = currYear;
            this.shortlist = new Shortlist(id);
            this.password = password;
        }
        public Student (string name, int id, int currYear, string password, Faculty major, Faculty minor) : this(name, id, currYear,password)
        {
            this.major = major;
            this.minor = minor;
        }

        public bool AddCourseToCompleted(Course course)
        {
            return completedCourses.AddCourseToCompleted(course);
        }

        public List<Course> GetCompletedCourses()
        {
            return completedCourses.GetCompletedCourses();
        }

        public bool AddCourseToShortlist(Course course)
        {
            return shortlist.AddCourseToShortlist(course);
        }

        public void RemoveCourseFromShortlist(Course course)
        {
            shortlist.RemoveCourseFromShortlist(course);
        }

        public List<Course> GetShortlist()
        {
            return shortlist.getShortlist();
        }
    }
}