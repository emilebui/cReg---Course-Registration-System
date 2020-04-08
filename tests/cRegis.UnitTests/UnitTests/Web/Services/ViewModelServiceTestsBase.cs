

using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class ViewModelServiceTestsBase : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public ViewModelServiceTestsBase()
        {
            _viewModelService = getContext();
        }

        protected IViewModelService getContext()
        {
            return new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context),
                new WishlistService(_context)
                );
        }
    }
}