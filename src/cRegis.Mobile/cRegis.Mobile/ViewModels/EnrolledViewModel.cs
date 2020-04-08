using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;

namespace cRegis.Mobile.ViewModels
{
    public class EnrolledViewModel
    {
        public int cid { get; set; }
        public int eid { get; set; }
        public string cName { get; set; }
        public string cDes { get; set; }
        public Course cour { get; set; }
        public Enrolled enroll { get; set; }
        
        public EnrolledViewModel(Course c, Enrolled e)
        {
            if (c != null && e != null)
            {
                cid = e.courseId;
                eid = e.enrollId;
                cName = c.courseName;
                cDes = c.courseDescription;
                cour = c;
                enroll = e;
            }
        }
    }
}
