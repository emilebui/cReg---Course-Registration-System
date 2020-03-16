﻿using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels.CourseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public IEnumerable<CourseContainerViewModel> cViewModels { get; set; }

        public ProfileViewModel(Student thisStudent, string majorName, IEnumerable <CourseContainerViewModel> cViewModels)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            this.cViewModels = cViewModels;
        }
    }
}
