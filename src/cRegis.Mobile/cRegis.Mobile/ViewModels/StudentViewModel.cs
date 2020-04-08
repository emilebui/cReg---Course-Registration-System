using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.ViewModels
{
    public class StudentViewModel
    {

        public string studentName { get; set; }
        public int studentID { get; set; }

        public string facultyName { get; set; }

        public string creditRemain { get; set; }

        public List<EnrolledViewModel> enrolledlist { get; set; }

        public StudentViewModel(Student s, string crehrs, List<EnrolledViewModel> listC, string fName)
        {
            if (s != null)
            {
                studentName = s.name;
                studentID = s.studentId;
            }
            facultyName = fName;
            creditRemain = crehrs;
            enrolledlist = listC;
        }

    }
}
