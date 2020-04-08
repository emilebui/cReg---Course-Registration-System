using cRegis.Core.Services;
using cRegis.Web.Controllers;
using cRegis.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net;

namespace cRegis.Tests.IntegrationTest.Infrastructure
{
    public class WebActionTestBase 
    {
        protected readonly CourseService _courseService;
        protected readonly StudentService _studentService;
        protected readonly EnrollService _enrollService;
        protected readonly FacultyService _facultyService;
        protected readonly WishlistService _wishlistService;
        protected readonly ViewModelService _viewModelService;
        protected readonly AuthController _authController;
        protected readonly HomeController _homeController;
        protected readonly CourseController _courseController;
        protected readonly DataContextTest _context;
        protected readonly FakeUserManager _userManager;
        protected readonly FakeSignInManager _signInManager;

        public WebActionTestBase()
        {
            var options = new DbContextOptionsBuilder<DataContextTest>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContextTest(options);
            _context.Database.EnsureCreated();

            _userManager = new FakeUserManager(_context);

            //mock httpcontext


            _signInManager = new FakeSignInManager(_userManager);

            _courseService = new CourseService(_context);
            _studentService = new StudentService(_context);
            _enrollService = new EnrollService(_context);
            _facultyService = new FacultyService(_context);
            _wishlistService = new WishlistService(_context);
            _viewModelService = new ViewModelService(_courseService, _enrollService, _studentService, _facultyService, _wishlistService);
            _courseController = new CourseController(_userManager, null, _courseService, _studentService, _enrollService, _wishlistService, _viewModelService);
            _homeController = new HomeController(_userManager, null, _courseService, _studentService, _enrollService, _viewModelService);
            _authController = new AuthController(_signInManager);
        }

    }
}
