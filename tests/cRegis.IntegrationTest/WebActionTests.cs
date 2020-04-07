using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.IntegrationTest
{ 
    public class WebActionTests : WebActionTestBase
    {
        public WebActionTests() { }

        [Theory]
        [InlineData("jb", 10, false)]
        [InlineData("pg", 28, true)]
        [InlineData("jb", 45, false)]
        public async Task DropCourseTest(string accountId, int enrollId, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
           {
                    new Claim(ClaimTypes.Name, accountId)
           }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Enrolled thisEnroll = await _enrollService.getEnrollAsync(enrollId);
            if (thisEnroll != null)
            {
                var action = (RedirectToActionResult)await _courseController.Drop(enrollId);

                //assert
                if (success)
                {
                    Assert.Equal("Success Drop", _courseController.TempData["alertMessage"]);
                    Assert.Null(_context.Enrolled.Find(enrollId));
                }
                else
                {
                    Assert.Equal("Failed Drop", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Index", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }

        }
        public async Task AutherizedLoginTest()
        {
            //arrange
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.Identity.IsAuthenticated).Returns(true);
            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user.Object }
            };

            //Act
            var result = (RedirectToActionResult)await _authController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);

        }

        [Theory]
        [InlineData("jb", "Password1!", true)]
        public async Task UnAutherizedLoginTest(string id, string password, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim(ClaimTypes.Name, id)
             }));
            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            //act
            var result = (RedirectToActionResult)await _authController.Index(new LoginViewModel { UserName = id, Password = password });

            if (success)
            {
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("Home", result.ControllerName);
            }
            else
            {
                Assert.NotNull(result);
                Assert.Null(result.ActionName);
                Assert.Equal("Home", result.ControllerName);
            }
        }

        [Theory]
        [InlineData(3, 75, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand")]
        [InlineData(17, 40, "The course thinks that everyone learns the same way (auditory). ")]
        public async Task GetRateTest(int eid, int rate, string comment)
        {
            //act
            ViewResult result = (ViewResult)await _courseController.Rate(eid);
            RateCourseViewModel model = (RateCourseViewModel)result.ViewData.Model;

            //assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.True(model.Rating.GetValueOrDefault() == rate);
            Assert.Equal(comment, model.Comment);

        }

        [Theory]
        [InlineData(9, 80, "Not bad")]
        public async Task SetRateTest(int eid, int rate, string comment)
        {
            //arrange

            RateCourseViewModel model = new RateCourseViewModel { EnrollId = eid, Rating = rate, Comment = comment };

            //act 

            var result = (RedirectToActionResult)await _courseController.Rate(model);
            Enrolled updatedEnroll = await _enrollService.getEnrollAsync(eid);

            //assert
            Assert.NotNull(result);
            Assert.NotNull(updatedEnroll);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.True(rate == updatedEnroll.rating);
            Assert.Equal(comment, updatedEnroll.comment);

        }

        [Theory]
        [InlineData("jb", 1, 10, true)]
        [InlineData("fb", 7, 6, false)]
        [InlineData("jb", 1, 45, false)]
        [InlineData("mz", 1, 15, false)]
        public async Task RegisterCourseTest(string accountId, int sid, int courseId, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, accountId)
            }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Course thisCourse = _courseService.getCourse(courseId);
            if (thisCourse != null)
            {
                var action = (RedirectToActionResult)await _courseController.Register(courseId);
                List<Enrolled> enrolls = _context.Enrolled.Where(e => e.studentId == sid && e.courseId == courseId && !e.completed).ToList();

                //assert
                if (success)
                {
                    Assert.Equal("Success Registration", _courseController.TempData["alertMessage"]);
                    Assert.NotEmpty(enrolls);
                    Assert.Null(_context.Wishlist.Find(sid, courseId));
                }
                else
                {
                    Assert.Equal("Failed Registration", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Register", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }
        }

        [Theory]
        [InlineData("jb", 1, 6, true)] //Happy Path
        [InlineData("mz", 2, 5, true)] //Happy Path
        [InlineData("jb", 1, 1, false)] //Entry Already Exists
        [InlineData("mz", 2, 1, false)] //Entry Already Exists
        public async Task AddCourseToWishlistTest(string accountId, int sid, int cid, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, accountId)
            }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Course thisCourse = _courseService.getCourse(cid);
            if (thisCourse != null)
            {
                int maxPriorityOrigin = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
                Wishlist wishlistEntryBefore = _context.Wishlist.SingleOrDefault(w => w.studentId == sid && w.courseId == cid);
                var action = (RedirectToActionResult)await _courseController.Add(cid);
                Wishlist wishlistEntryAfter = _context.Wishlist.SingleOrDefault(w => w.studentId == sid && w.courseId == cid);

                //assert
                if (success)
                {
                    Assert.Null(wishlistEntryBefore);
                    Assert.Equal("Course Added to Wishlist", _courseController.TempData["alertMessage"]);
                    Assert.NotNull(wishlistEntryAfter);
                    int newEntryPriority = wishlistEntryAfter.priority;
                    Assert.True(newEntryPriority == maxPriorityOrigin + 1);

                }
                else
                {
                    Assert.Equal("Course is Already in Wishlist", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Register", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }
        }

        [Theory]
        [InlineData("jb", 1, 1, true)] //Happy Path
        [InlineData("mz", 2, 1, true)] //Happy Path
        [InlineData("jb", 1, 6, false)] //Non-Existent Entry
        [InlineData("mz", 2, 6, false)] //Non-Existent Entry
        public async Task RemoveCourseFromWishlistTest(string accountId, int sid, int cid, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, accountId)
            }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Course thisCourse = _courseService.getCourse(cid);
            if (thisCourse != null)
            {
                var action = (RedirectToActionResult)await _courseController.Remove(cid);

                //assert
                if (success)
                {
                    Assert.Equal("Course was Removed From Wishlist", _courseController.TempData["alertMessage"]);
                    Assert.Null(_context.Wishlist.Find(sid, cid));
                }
                else
                {
                    Assert.Equal("Course is Not in Wishlist", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Wishlist", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }
        }

        [Theory]
        [InlineData("jb", 1, 2, MoveDirection.MoveUp, true, true)] //Happy Path Move Up Update
        [InlineData("jb", 1, 1, MoveDirection.MoveUp, false, true)] //Happy Path Move Up No Update
        [InlineData("jb", 1, 20, MoveDirection.MoveUp, true, false)] //Non-Existent Entry Move Up Update
        [InlineData("jb", 1, 20, MoveDirection.MoveUp, false, false)] //Non-Existent Entry Move Up No Update
        [InlineData("jb", 1, 2, MoveDirection.MoveDown, true, true)] //Happy Path Move Down Update
        [InlineData("jb", 1, 5, MoveDirection.MoveDown, false, true)] //Happy Path Move Down No Update
        [InlineData("jb", 1, 20, MoveDirection.MoveDown, true, false)] //Non-Existent Entry Move Down Update
        [InlineData("jb", 1, 20, MoveDirection.MoveDown, false, false)] //Non-Existent Entry Move Up Down Update
        public async Task MoveWishlistEntryPriorityTest(string accountId, int sid, int cid, MoveDirection direction, bool update, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, accountId)
            }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Course thisCourse = _courseService.getCourse(cid);
            if (thisCourse != null)
            {
                var action = default(RedirectToActionResult);
                Wishlist entryToMove = _context.Wishlist.SingleOrDefault(w => w.studentId == sid && w.courseId == cid);
                //assert
                if (success)
                {
                    Assert.NotNull(entryToMove);
                    int entryToMovePriority = entryToMove.priority;
                    Assert.True(entryToMovePriority > 0);

                    action = (RedirectToActionResult)await _courseController.Move(cid, direction);

                    Assert.NotNull(entryToMove);
                    int entryToMovePriorityUpdated = entryToMove.priority;
                    Assert.True(entryToMovePriority > 0);
                    if (update && direction == MoveDirection.MoveUp)
                    {
                        Assert.True(entryToMovePriorityUpdated == entryToMovePriority - 1);
                    }
                    else if (update && direction == MoveDirection.MoveDown)
                    {
                        Assert.True(entryToMovePriorityUpdated == entryToMovePriority + 1);
                    }
                    else if (!update && (direction == MoveDirection.MoveUp || direction == MoveDirection.MoveDown))
                    {
                        Assert.True(entryToMovePriorityUpdated == entryToMovePriority);
                    }
                }
                else
                {
                    Assert.Null(entryToMove);
                    action = (RedirectToActionResult)await _courseController.Move(cid, direction);
                    Assert.Null(entryToMove);
                }
                Assert.NotNull(action);
                Assert.Equal("Wishlist", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }
        }
    }
}
