using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Mobile
{
    public class CourseDetailViewModelTest
    {

        [Fact]
        public void validateModel()
        {
            Course c = new Course() { courseDescription = "yikes", courseId = 1, courseName = "COMP", creditHours = 2, space = 5 };
            List<Comment> listE = new List<Comment>();
            CourseDetailViewModel model = new CourseDetailViewModel(c, listE);


            Assert.Equal("yikes", model.courseDescription);
            Assert.Equal("COMP", model.courseName);
            Assert.True(model.courseId == 1);
            Assert.True(model.creditHours == 2);
            Assert.True(model.space == 5);
            Assert.True(model.commentList == listE);
            Assert.True(model.chosenCourse == c);
        }

        [Fact]
        public void validateModelForNULL()
        {
            Course c = null;
            List<Comment> listE = null;
            CourseDetailViewModel model = new CourseDetailViewModel(c, listE);
            Assert.True(model.courseId == 0);
            Assert.True(model.creditHours == 0);
            Assert.True(model.space == 0);
            Assert.True(model.courseName == null);
            Assert.True(model.courseDescription == null);
            Assert.True(model.commentList == null);
            Assert.True(model.chosenCourse == null);
        }
    }
}
