using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Mobile
{
    public class EnrolledViewModelTest
    {

        [Fact]
        public void validateModel()
        {
            Course c = new Course() { courseDescription = "yikes", courseId = 1, courseName = "COMP", creditHours = 2, space = 5 };
            Enrolled e = new Enrolled() { studentId = 1, courseId = 1, enrollId = 1, completed = true, course = c, grade = 90 };
            EnrolledViewModel model = new EnrolledViewModel(c, e);
            Assert.Equal("yikes", model.cDes);
            Assert.Equal("COMP", model.cName);
            Assert.True(model.eid == 1);
            Assert.True(model.cid == 1);
        }

        [Fact]
        public void validateModelForNULL()
        {
            Course c = null;
            Enrolled e = null;
            EnrolledViewModel model = new EnrolledViewModel(c, e);
            Assert.True(model.eid == 0);
            Assert.True(model.cid == 0);
            Assert.True(model.cour == null);
            Assert.True(model.enroll == null);
        }
    }
}
