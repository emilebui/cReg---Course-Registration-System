using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;

namespace cRegis.IntegrationTest
{
    public class MobileIntegrationTest
    {
        private IAuthService _authService;
        private string token;

        private async Task<bool> Init()
        {
            _authService = new AuthService();
            HttpResponseMessage response = await _authService.jwtAuthenticate("jb", "Password1!");

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }

        private async void testStudentServices()
        {
            //GET /student
            IStudentService testService = new StudentService(token);
            Student s = await testService.getStudentAsync();

            Assert.Equal("John Braico", s.name);
            Assert.True(s.studentId == 1);
            Assert.True(s.majorId == 1);


            //Get student/credhrs
            string str = await testService.getStudentCreditAsync();
            Assert.Equal("24", str);

            //Get faculty
            Faculty f = await testService.getFaculty(s.majorId);
            Assert.True(f != null);

            //Get enrolledList
            List<Enrolled> l = await testService.getStudentEnrolledListAsync();
            Assert.True(l != null);

            //Get courseList
            List<Course> lc = await testService.getStudentCourseListAsync();
            Assert.True(lc != null);

            //Get course
            Course c = await testService.getCourseAsync(1);
            Assert.True(c != null);
        }

        private async void testCourseServices()
        {
            ICourseService testService = new CourseService(token);

            //Get HistoryCourse
            List<Course> l = await testService.getHistoryListAsync();
            Assert.True(l != null);

            //Get CourseList
            List<Course> lc = await testService.getCourseListAsync();
            Assert.True(lc != null);

            //Get CommentList
            List<Comment> lcom = await testService.getCourseCommentAsync(1);
            Assert.True(lcom != null);

        }

        private async void testHistoryServices()
        {
            IHistoryService _historyService = new HistoryService(token);

            List<Enrolled> l = await _historyService.getHistoryEnrolledListAsync();
            Assert.True(l != null);
        }


        [Fact]
        public async void testServices()
        {
            bool check = await Init();
            if (check)
            {
                testStudentServices();
                testCourseServices();
                testHistoryServices();
            }

        }

    }
}
