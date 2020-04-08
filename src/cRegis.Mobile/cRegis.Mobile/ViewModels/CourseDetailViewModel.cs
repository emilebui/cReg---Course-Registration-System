using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;

namespace cRegis.Mobile.ViewModels
{
    public class CourseDetailViewModel
    {
        public Course chosenCourse { get; set; }
        public List<Comment> commentList { get; set; }

        public int courseId { get; set; }

        public string courseName { get; set; }

        public string courseDescription { get; set; }
        public int creditHours { get; set; }
        public int space { get; set; }
        public DateTime date { get; set; }

        public CourseDetailViewModel(Course c, List<Comment> l)
        {
            chosenCourse = c;
            commentList = l;

            if (c != null)
            {
                courseId = c.courseId;
                courseName = c.courseName;
                courseDescription = c.courseDescription;
                creditHours = c.creditHours;
                space = c.space;
                date = c.date;
            }
        }
    }
}
