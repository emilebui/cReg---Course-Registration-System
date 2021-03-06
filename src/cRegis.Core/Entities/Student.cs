﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Entities
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }

        public int majorId { get; set; }
        public string name { get; set; }

        public Faculty major { get; set; }
        public ICollection<Enrolled> enrolledCourses { get; set; }
        
    }
}
