using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Interfaces
{
    public enum MoveDirection
    {
        MoveUp,
        MoveDown
    }

    public interface IWishlistService
    {
        Task<Wishlist> getWishlistByKeys(int sid, int cid);

        Task<int> addCoursetoStudentWishlist(int sid, int cid);

        Task<int> updatePriority(int sid, int cid, MoveDirection direction);

        Task<int> verifyWishlistEntry(int sid, int cid);

        Wishlist removeCourseFromStudentWishlist(int sid, int cid);

        List<Wishlist> getStudentWishlist(int sid);
    }
}
