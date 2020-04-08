using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Mobile
{
    public class StudentViewModelTest
    {

        [Fact]
        public void validateModel()
        {
            Student s = new Student() { name = "Yikes", majorId = 1, studentId = 1 };
            string name = "YEET";
            string cre = "100";
            List<EnrolledViewModel> listE = new List<EnrolledViewModel>();
            StudentViewModel model = new StudentViewModel(s, cre, listE, name);

            Assert.Equal("YEET", model.facultyName);
            Assert.Equal("Yikes", model.studentName);
            Assert.True(model.studentID == 1);
            Assert.Equal("100", model.creditRemain);
            Assert.True(model.enrolledlist == listE);


        }

        [Fact]
        public void validateModelForNULL()
        {
            Student s = null;
            string name = null;
            string cre = null;
            List<EnrolledViewModel> listE = null;
            StudentViewModel model = new StudentViewModel(s, cre, listE, name);
            Assert.True(model.enrolledlist == null);
            Assert.True(model.facultyName == null);
            Assert.True(model.creditRemain == null);
            Assert.True(model.studentName == null);
        }
    }
}
