using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Mobile
{
    public class HistoryViewModelTest
    {

        [Fact]
        public void validateModel()
        {
            List<EnrolledViewModel> listC = new List<EnrolledViewModel>();
            HistoryViewModel model = new HistoryViewModel(listC);

            Assert.True(model.AllCourses == listC);
        }

        [Fact]
        public void validateModelForNULL()
        {
            List<EnrolledViewModel> listC = null;
            HistoryViewModel model = new HistoryViewModel(listC);

            Assert.True(model.AllCourses == null);
        }
    }
}
