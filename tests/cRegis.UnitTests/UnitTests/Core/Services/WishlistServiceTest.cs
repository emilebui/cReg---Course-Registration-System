using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{
    public class WishlistServiceTest : TestBase
    {
        private readonly WishlistService _wishlistService;

        public WishlistServiceTest()
        {
            _wishlistService = new WishlistService(_context);
        }

        //addCoursetoStudentWishlistTest()
        [Fact]
        public async void addCoursetoStudentWishlistTest_HappyPath()
        {
            int outcome = await _wishlistService.addCoursetoStudentWishlist(2, 6);
            Assert.True(outcome == 0);
            List<int> wishlist = _context.Wishlist.Where(w => w.studentId == 2).Select(w => w.courseId).ToList();
            Assert.True(wishlist.Contains(6), "student 2 has course 6 in wishlist");
        }

        [Fact]
        public async void addCoursetoStudentWishlistTest_FalseOutcome()
        {
            int outcome = await _wishlistService.addCoursetoStudentWishlist(2, 6);
            Assert.True(outcome == 0);
            List<int> wishlist = _context.Wishlist.Where(w => w.studentId == -1).Select(w => w.courseId).ToList();
            Assert.False(wishlist.Contains(6), "student 2 has course 6 in wishlist");
        }

        [Fact]
        public async void addCoursetoStudentWishlistTest_NonExistentStudent()
        {
            int outcome = await _wishlistService.addCoursetoStudentWishlist(-1, 6);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void addCoursetoStudentWishlistTest_NonExistentCourse()
        {
            int outcome = await _wishlistService.addCoursetoStudentWishlist(2, -1);
            Assert.True(outcome == 2);
        }

        [Fact]
        public async void addCoursetoStudentWishlistTest_EntryAlreadyExists()
        {
            int initialOutcome = await _wishlistService.addCoursetoStudentWishlist(2, 6);
            Assert.True(initialOutcome == 0);
            int finalOutcome = await _wishlistService.addCoursetoStudentWishlist(2, 6);
            Assert.True(finalOutcome == 3);
        }

        //updatePriorityTest()
        [Fact]
        public async void updatePriorityTest_MoveUp_HappyPath()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 2;
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.courseId == courseId);
            Assert.NotNull(entry);
            //Get Priority
            int entryPriority = entry.priority;
            Assert.True(entryPriority > 0);
            //Move Priority Up
            MoveDirection direction = MoveDirection.MoveUp;
            int outcome = await _wishlistService.updatePriority(entry.studentId, entry.courseId, direction);
            Assert.True(outcome == 0, "priority of course 2 has moved up in student 1's wishlist");
            //Double Check
            Assert.True(entry.priority == entryPriority - 1);
        }

        [Fact]
        public async void updatePriorityTest_MoveUp_NonExistentStudent()
        {
            int studentId = -1;
            int courseId = 1;
            MoveDirection direction = MoveDirection.MoveUp;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void updatePriorityTest_MoveUp_NonExistentCourse()
        {
            int studentId = 1;
            int courseId = -1;
            MoveDirection direction = MoveDirection.MoveUp;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 2);
        }

        [Fact]
        public async void updatePriorityTest_MoveUp_NonExistentEntry()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 20;
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.courseId == courseId);
            Assert.Null(entry);
            //Move Priority Up
            MoveDirection direction = MoveDirection.MoveUp;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 3);
        }

        [Fact]
        public async void updatePriorityTest_MoveUp_NoAction()
        {
            //Get Entry
            int studentId = 1;
            int maxPriority = 1;
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == maxPriority);
            Assert.NotNull(entry);
            //Move Priority Up
            MoveDirection direction = MoveDirection.MoveUp;
            int outcome = await _wishlistService.updatePriority(entry.studentId, entry.courseId, direction);
            Assert.True(outcome == 4);
        }

        [Fact]
        public async void updatePriorityTest_MoveDown_HappyPath()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 2;
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.courseId == courseId);
            Assert.NotNull(entry);
            //Get Priority
            int entryPriority = entry.priority;
            Assert.True(entryPriority > 0);
            //Move Priority Down
            MoveDirection direction = MoveDirection.MoveDown;
            int outcome = await _wishlistService.updatePriority(entry.studentId, entry.courseId, direction);
            Assert.True(outcome == 0, "priority of course 2 has moved down in student 1's wishlist");
            //Double Check
            Assert.True(entry.priority == entryPriority + 1);
        }

        [Fact]
        public async void updatePriorityTest_MoveDown_NonExistentStudent()
        {
            int studentId = -1;
            int courseId = 1;
            MoveDirection direction = MoveDirection.MoveDown;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void updatePriorityTest_MoveDown_NonExistentCourse()
        {
            int studentId = 1;
            int courseId = -1;
            MoveDirection direction = MoveDirection.MoveDown;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 2);
        }

        [Fact]
        public async void updatePriorityTest_MoveDown_NonExistentEntry()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 20;
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.courseId == courseId);
            Assert.Null(entry);
            //Move Priority Down
            MoveDirection direction = MoveDirection.MoveDown;
            int outcome = await _wishlistService.updatePriority(studentId, courseId, direction);
            Assert.True(outcome == 3);
        }

        [Fact]
        public async void updatePriorityTest_MoveDown_NoAction()
        {
            //Get List
            int studentId = 1;
            List<Wishlist> wishlist = _context.Wishlist.Where(w => w.studentId == studentId).ToList();
            Assert.NotEmpty(wishlist);
            //Get Min Priority
            int minPriority = wishlist.Max(w => w.priority);
            Assert.True(minPriority > 0);
            //Get Entry
            Wishlist entry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == minPriority);
            Assert.NotNull(entry);
            //Move Priority Down
            MoveDirection direction = MoveDirection.MoveDown;
            int outcome = await _wishlistService.updatePriority(entry.studentId, entry.courseId, direction);
            Assert.True(outcome == 5);
        }

        //verifyWishlistEntryTest()
        [Fact]
        public async void verifyWishlistEntryTest_HappyPath()
        {
            int studentId = 2;
            int courseId = 3;
            Wishlist entry = _context.Wishlist.Find(studentId, courseId);
            Assert.NotNull(entry);
            int outcome = await _wishlistService.verifyWishlistEntry(entry.studentId, entry.courseId);
            Assert.True(outcome == 0, "course 3 is in student 2's wishlist");
        }

        [Fact]
        public async void verifyWishlistEntryTest_NonExistantStudent()
        {
            int studentId = -1;
            int courseId = 3;
            int outcome = await _wishlistService.verifyWishlistEntry(studentId, courseId);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void verifyWishlistEntryTest_NonExistantCourse()
        {
            int studentId = 2;
            int courseId = -1;
            int outcome = await _wishlistService.verifyWishlistEntry(studentId, courseId);
            Assert.True(outcome == 2);
        }

        [Fact]
        public async void verifyWishlistEntryTest_NonExistentEntry()
        {
            int studentId = 2;
            int courseId = 9;
            Wishlist entry = _context.Wishlist.Find(studentId, courseId);
            Assert.Null(entry);
            int outcome = await _wishlistService.verifyWishlistEntry(studentId, courseId);
            Assert.True(outcome == 3);
        }

        //removeCourseFromStudentWishlistTest()
        [Fact]
        public void removeCourseFromStudentWishlist_HappyPath_PriorityChange()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 1;
            Wishlist entryToRemove = _context.Wishlist.Find(studentId, courseId);
            Assert.NotNull(entryToRemove);
            //Get Entry To Remove Priority
            int entryToRemovePriority = entryToRemove.priority;
            Assert.True(entryToRemovePriority > 0);
            //Get Entry With One Lower Priority
            int nextEntryPriority = entryToRemove.priority + 1;
            Wishlist nextEntry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == nextEntryPriority);
            Assert.NotNull(nextEntry);
            Assert.True(nextEntry.courseId != entryToRemove.courseId);
            //Remove Entry
            Wishlist removedEntry = _wishlistService.removeCourseFromStudentWishlist(entryToRemove.studentId, entryToRemove.courseId);
            //Check If Removed Entry Exists
            Assert.NotNull(removedEntry);
            //Check If Next Entry Priority Is Set To Removed Entry's Priority
            Assert.True(nextEntry.priority == entryToRemovePriority);
        }

        [Fact]
        public void removeCourseFromStudentWishlist_HappyPath_NoPriorityChange()
        {
            //Get Last Entry
            int studentId = 1;
            List<Wishlist> wishlistEntries = _context.Wishlist.Where(w=> w.studentId == studentId).ToList();
            Assert.NotEmpty(wishlistEntries);
            int entryToRemovePriority = wishlistEntries.Max(w=> w.priority);
            Assert.True(entryToRemovePriority > 0);
            Wishlist entryToRemove = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == entryToRemovePriority);
            Assert.NotNull(entryToRemove);
            //Get Entry With One Lower Priority (Null)
            int nextEntryPriority = entryToRemove.priority + 1;
            Wishlist nextEntry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == nextEntryPriority);
            Assert.Null(nextEntry);
            //Get Entry With One Higher Priority 
            int prevEntryPriority = entryToRemove.priority - 1;
            Wishlist prevEntry = _context.Wishlist.SingleOrDefault(w => w.studentId == studentId && w.priority == prevEntryPriority);
            Assert.NotNull(prevEntry);
            //Remove Entry
            Wishlist removedEntry = _wishlistService.removeCourseFromStudentWishlist(entryToRemove.studentId, entryToRemove.courseId);
            //Check If Removed Entry Exists
            Assert.NotNull(removedEntry);
            //Check If Previous Entry Did Not Change Priority
            Assert.True(prevEntry.priority == prevEntryPriority);
        }

        [Fact]
        public void removeCourseFromStudentWishlist_NonExistentEntry()
        {
            //Get Entry
            int studentId = 1;
            int courseId = 28;
            Wishlist nullEntry = _context.Wishlist.Find(studentId, courseId);
            Assert.Null(nullEntry);
            //Remove Entry
            Wishlist removedEntry = _wishlistService.removeCourseFromStudentWishlist(studentId, courseId);
            //Check If Entry Exists
            Assert.Null(removedEntry);
        }

        //getStudentWishlistTest()
        [Fact]
        public void getStudentWishlist_HappyPath()
        {
            int studentId = 1;
            List<Wishlist> studentWishlist = _wishlistService.getStudentWishlist(studentId);
            Assert.NotEmpty(studentWishlist);
            //Check the courseId and priority for each element in the list
            Assert.True(studentWishlist.Count == 5);
            //0
            Assert.True(studentWishlist[0].courseId == 1);
            Assert.True(studentWishlist[0].priority == 1);
            //1
            Assert.True(studentWishlist[1].courseId == 2);
            Assert.True(studentWishlist[1].priority == 2);
            //2
            Assert.True(studentWishlist[2].courseId == 3);
            Assert.True(studentWishlist[2].priority == 3);
            //3
            Assert.True(studentWishlist[3].courseId == 4);
            Assert.True(studentWishlist[3].priority == 4);
            //4
            Assert.True(studentWishlist[4].courseId == 5);
            Assert.True(studentWishlist[4].priority == 5);
        }

        [Fact]
        public void getStudentWishlistTest_NonExistentStudent()
        {
            int studentId = -1;
            List<Wishlist> studentWishlist = _wishlistService.getStudentWishlist(studentId);
            Assert.True(studentWishlist.Count == 0);
        }

        //getWsihlistByKeysTest()
        [Fact]
        public async void getgetWishlistByKeysTest_HappyPath()
        {
            int studentId = 1;
            int courseId = 1;
            Wishlist entry = await _wishlistService.getWishlistByKeys(studentId, courseId);
            Assert.NotNull(entry);
            Assert.True(entry.priority == 1);
        }

        [Fact]
        public async void getgetWishlistByKeysTest_NonExistentEntry()
        {
            int studentId = 1;
            int courseId = 10;
            Wishlist entry = await _wishlistService.getWishlistByKeys(studentId, courseId);
            Assert.Null(entry);
        }
    }
}
