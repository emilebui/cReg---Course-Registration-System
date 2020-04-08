using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Web.Interfaces;
using cRegis.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace cRegis.Web.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollService _enrollService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly IWishlistService _wishlistService;

        public ViewModelService(ICourseService courseService,
            IEnrollService enrollService,
            IStudentService studentService,
            IFacultyService facultyService, IWishlistService wishlistService)
        {
            _facultyService = facultyService;
            _studentService = studentService;
            _courseService = courseService;
            _enrollService = enrollService;
            _wishlistService = wishlistService;
        }

        public CourseCommentViewModel buildCourseCommentViewModel(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new CourseCommentViewModel
            {
                ratingScore = comment.ratingScore,
                comment = comment.comment,
                takenDate = comment.takenDate
            };
        }

        public CourseContainerViewModel buildCourseContainerViewModel(Course course, ISet<CourseActions> actions, Enrolled enroll = null, Student student = null)
        {
            if (course == null)
            {
                return null;
            }

            return new CourseContainerViewModel
            {
                courseId = course.courseId,
                courseName = course.courseName,
                courseDescription = course.courseDescription,
                actions = actions,
                enrollId = enroll?.enrollId,
                studentId = student?.studentId
            };
        }

        public CourseDetailViewModel buildCourseDetailViewModel(int cid)
        {
            Course course = _courseService.getCourse(cid);
            if (course == null)
            {
                return null;
            }

            List<CourseCommentViewModel> commentsVM = null;
            string avgRating = "N/A";
            List<Comment> comments = _courseService.getCommentsForCourse(course.courseId);

            if (comments.Count > 0)
            {
                commentsVM = new List<CourseCommentViewModel>();

                int ratingSum = 0;
                foreach (Comment cmt in comments)
                {
                    ratingSum += cmt.ratingScore;
                    commentsVM.Add(buildCourseCommentViewModel(cmt));
                }
                avgRating = (ratingSum / comments.Count).ToString() + "/100";
            }

            return new CourseDetailViewModel
            {
                courseName = course.courseName,
                courseDescription = course.courseDescription,
                availableSpace = _courseService.getAvailableSpaceForCourse(course.courseId),
                date = course.date,
                avgRating = avgRating,
                comments = commentsVM,
            };
        }

        public async Task<FindCourseViewModel> buildFindCourseViewModelAsync(Student student)
        {
            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> courseContainerViewModels = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions;
            List<Course> eligibleCourses = await _courseService.getRecCoursesForStudentAsync(student.studentId);

            foreach (Course course in eligibleCourses)
            {
                var inWishList = await _wishlistService.getWishlistByKeys(student.studentId, course.courseId);
                // if course already in wishlist, then disable it
                if (inWishList != null)
                {
                    actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse, CourseActions.AddToWishListDisabled };
                }
                else
                {
                    actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse, CourseActions.AddToWishlist };

                }
                courseContainerViewModels.Add(buildCourseContainerViewModel(course, actions, student: student));
            }

            return new FindCourseViewModel
            {
                courseList = courseContainerViewModels
            };
        }

        public HistoryViewModel buildHistoryViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> courseContainerViewModels = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.RateCourse, CourseActions.ViewDetail };
            List<Enrolled> completed = _enrollService.getCompletedEnrollsForStudent(student.studentId);

            foreach (Enrolled enroll in completed)
            {
                Course course = _courseService.getCourse(enroll.courseId);
                courseContainerViewModels.Add(buildCourseContainerViewModel(course, actions, enroll: enroll));
            }

            return new HistoryViewModel
            {
                thisStudent = student,
                courses = courseContainerViewModels
            };
        }

        public WishlistViewModel buildWishlistViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> {CourseActions.ViewDetail, CourseActions.RegisterCourse, CourseActions.WishlistPriorityUp, CourseActions.WishlistPriorityDown, CourseActions.RemoveFromWishlist};
            List<Wishlist> wishlist = _wishlistService.getStudentWishlist(student.studentId);
     
            foreach (Wishlist entry in wishlist)
            {
                Course course = _courseService.getCourse(entry.courseId);
                var ccvm = buildCourseContainerViewModel(course, actions, student: student);
                ccvms.Add(ccvm);
            }

            WishlistViewModel vmodel = new WishlistViewModel { thisStudent = student, courses = ccvms };

            return vmodel;
        }
        public ProfileViewModel buildProfileViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            int remainCreditHours = _studentService.getRemainingCredithoursForStudent(student.studentId);

            string majorName = "";
            Faculty tempFaculty = _facultyService.getFaculty(student.majorId);
            if (tempFaculty != null)
            {
                majorName = tempFaculty.facultyName;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.DropCourse };
            List<Enrolled> regCourses = _enrollService.getCurrentEnrollsForStudent(student.studentId);

            foreach (Enrolled e in regCourses)
            {
                Course thisCourse = _courseService.getCourse(e.courseId);
                CourseContainerViewModel ccvm = buildCourseContainerViewModel(thisCourse, actions, e);
                ccvms.Add(ccvm);
            }

            return new ProfileViewModel
            {
                thisStudent = student,
                majorName = majorName,
                cViewModels = ccvms,
                remainingCreditHours = remainCreditHours
            };
        }

        public RateCourseViewModel buildRateCourseViewModel(Enrolled rate, Course course)
        {
            if (rate == null)
            {
                return null;
            }
            if (course == null)
            {
                return null;
            }
            return new RateCourseViewModel
            {
                EnrollId = rate.enrollId,
                Rating = rate.rating,
                Comment = rate.comment,
                CourseName = course.courseName,
                courseDescription = course.courseDescription
            };
        }
    }
}