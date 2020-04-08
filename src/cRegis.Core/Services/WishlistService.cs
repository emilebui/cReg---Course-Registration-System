using cRegis.Core.Data;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Services
{
    public class WishlistService: IWishlistService
    {
        private readonly DataContext _context;

        public WishlistService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> addCoursetoStudentWishlist(int sid, int cid)
        {
            if (await _context.Students.FindAsync(sid) == null)
            {
                return 1;
            }
            if (await _context.Courses.FindAsync(cid) == null)
            {
                return 2;
            }
            if(await _context.Wishlist.FindAsync(sid, cid) != null)
            {
                return 3;
            }
            int lastPriorityNum = 0;
            if (_context.Wishlist.Any(w => w.studentId == sid))
            {
                lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
            }
            Wishlist newWishlistEntry = new Wishlist {studentId = sid, courseId = cid, priority = lastPriorityNum+1};
            _context.Wishlist.Add(newWishlistEntry);
            _context.SaveChanges();
            return 0;
        }

        public async Task<int> updatePriority(int sid, int cid, MoveDirection direction)
        {
            if (await _context.Students.FindAsync(sid) == null)
            {
                return 1;
            }
            if (await _context.Courses.FindAsync(cid) == null)
            {
                return 2;
            }

            Wishlist sourceEntry = _context.Wishlist.Find(sid, cid);
            if(sourceEntry == null)
            {
                return 3;
            }
            int sourceEntryPriority = sourceEntry.priority;
            int lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
            int destinationEntryPriority = -1;
            if (direction == MoveDirection.MoveUp && sourceEntryPriority > 1)
            {
                destinationEntryPriority = sourceEntryPriority - 1;
            }
            else if (direction == MoveDirection.MoveUp && sourceEntryPriority <= 1)
            {
                return 4;
            }
            else if (direction == MoveDirection.MoveDown && sourceEntryPriority < lastPriorityNum)
            {
                destinationEntryPriority = sourceEntryPriority + 1;
            }
            else if (direction == MoveDirection.MoveDown && sourceEntryPriority >= lastPriorityNum)
            {
                return 5;
            }

            Wishlist destinationEntry = _context.Wishlist.SingleOrDefault(w => w.studentId == sid && w.priority == destinationEntryPriority);
            sourceEntry.priority = destinationEntryPriority;
            destinationEntry.priority = sourceEntryPriority;
            var change1 = _context.Wishlist.Update(sourceEntry);
            var change2 = _context.Wishlist.Update(destinationEntry);
            _context.SaveChanges();
            return 0;
        }

        public async Task<int> verifyWishlistEntry(int sid, int cid)
        {
            if (await _context.Students.FindAsync(sid) == null)
            {
                return 1;
            }
            if (await _context.Courses.FindAsync(cid) == null)
            {
                return 2;
            }
            if (await _context.Wishlist.FindAsync(sid, cid) == null)
            {
                return 3;
            }
            return 0;
        }


        public Wishlist removeCourseFromStudentWishlist(int sid, int cid)
        {
            Wishlist thisEntry = _context.Wishlist.Find(sid, cid);
            if (thisEntry != null)
            {
                _context.Wishlist.Remove(thisEntry);
                int lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
                if (thisEntry.priority < lastPriorityNum)
                {
                    IOrderedEnumerable<Wishlist> entriesToModify = _context.Wishlist.Where(w => w.studentId == sid && w.priority > thisEntry.priority).ToList().OrderBy(w => w.priority);
                    foreach (Wishlist entry in entriesToModify)
                    {
                        entry.priority = entry.priority - 1;
                        _context.Wishlist.Update(entry);
                    }
                }
                _context.SaveChanges();
            }
            return thisEntry;
        }

        public List<Wishlist> getStudentWishlist(int sid)
        {
            List<Wishlist> wishlist = _context.Wishlist.Where(w => w.studentId == sid).ToList();
            IOrderedEnumerable<Wishlist> orderedWishlist = wishlist.OrderBy(w => w.priority);
            return orderedWishlist.ToList();
        }

        public async Task<Wishlist> getWishlistByKeys(int sid, int cid)
        {
            return await _context.Wishlist.FindAsync(sid, cid);
        }
    }
}
