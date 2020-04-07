using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace cRegis.API.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [Route("[controller]/add/{cid}")]
        [HttpPost]
        public async Task<ActionResult> addCoursetoStudentWishlist(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            int valid = await _wishlistService.verifyWishlistEntry(sid, cid);

            if (valid < 3)
            {
                return BadRequest("Student is not able to add the course to the wishlist");
            }

            await _wishlistService.addCoursetoStudentWishlist(sid, cid);
            return Ok("Successful Addition To Wishlist");
        }

        [Route("[controller]/moveUp/{cid}")]
        [HttpPost]
        public async Task<ActionResult> updatePriorityUp(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            await _wishlistService.updatePriority(sid, cid, MoveDirection.MoveUp);
            return Ok();
        }

        [Route("[controller]/moveDown/{cid}")]
        [HttpPost]
        public async Task<ActionResult> updatePriorityDown(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            await _wishlistService.updatePriority(sid, cid, MoveDirection.MoveDown);
            return Ok();
        }

        [Route("[controller]/remove/{cid}")]
        [HttpDelete]
        public ActionResult removeCourseFromStudentWishlist(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            Wishlist entryToRemove = _wishlistService.removeCourseFromStudentWishlist(sid, cid);

            if (entryToRemove == null)
            {
                return BadRequest("Student is not able to remove the course to the wishlist");
            }
            return Ok("Successful Removal From Wishlist");
        }

        [Route("[controller]")]
        [HttpGet]
        public ActionResult<List<Wishlist>> getStudentWishlist()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var result = _wishlistService.getStudentWishlist(sid);

            return result;
        }
    }
}
