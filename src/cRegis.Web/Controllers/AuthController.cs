using System.Threading.Tasks;
using cRegis.Core.Identities;
using cRegis.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cRegis.Web.Controllers
{
    public class AuthController : Controller
    {
  
        private readonly SignInManager<StudentUser> _signInManager;

        public AuthController(SignInManager<StudentUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //if user is already logged in
            bool loggedIn = this.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            return await Task.Run(() => View());
        }

        
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            //for unknown reason login failed
            return RedirectToAction("Index");

        }

        
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return StatusCode(401);
        }
    }
}