using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Mobile
{
    public class CourseViewModelTest
    {

        [Fact]
        public void validateModel()
        {
            List<Course> listC = new List<Course>();
            CourseViewModel model = new CourseViewModel(listC);

            Assert.True(model.AllCourses == listC);
        }

        [Fact]
        public void validateModelForNULL()
        {
            List<Course> listC = null;
            CourseViewModel model = new CourseViewModel(listC);

            Assert.True(model.AllCourses == null);
        }
    }
}
