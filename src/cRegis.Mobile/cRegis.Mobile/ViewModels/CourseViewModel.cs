using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;

namespace cRegis.Mobile.ViewModels
{
    public class CourseViewModel
    {
        public List<Course> AllCourses { get; set; }

        public CourseViewModel(List<Course> l)
        {
            AllCourses = l;
        }
    }
}
