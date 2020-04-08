using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cRegis.Web.Interfaces
{
    public enum CourseActions
    {
        ViewDetail,
        RateCourse,
        DropCourse,
        RegisterCourse,
        AddToWishlist,
        WishlistPriorityUp,
        WishlistPriorityDown,
        RemoveFromWishlist,
        AddToWishListDisabled
    }

    public interface IViewModelService
    {
        RateCourseViewModel buildRateCourseViewModel(Enrolled rate, Course course);

        CourseDetailViewModel buildCourseDetailViewModel(int cid);

        ProfileViewModel buildProfileViewModel(Student student);

        Task<FindCourseViewModel> buildFindCourseViewModelAsync(Student student);

        HistoryViewModel buildHistoryViewModel(Student student);

        WishlistViewModel buildWishlistViewModel(Student student);

        CourseCommentViewModel buildCourseCommentViewModel(Comment comment);

        CourseContainerViewModel buildCourseContainerViewModel(Course course, ISet<CourseActions> actions, Enrolled enroll = null, Student s = null);
    }
}
