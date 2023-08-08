using _3gim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3gim.Controllers
{
    [Route("member")]
    public class MemberController : Controller
    {
        private readonly UserManager<_3gimMember> _userManager;
        private readonly SignInManager<_3gimMember> _signInManager;

        public MemberController(
           UserManager<_3gimMember> userManager,
           SignInManager<_3gimMember> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        


        [HttpGet("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(Signup model)
        {
            var user = new _3gimMember    

            { Id=model.name, UserName = model.id, Email = model.email };

            var result = await _userManager.CreateAsync(user, model.password);

            if (result.Succeeded)
            {
                return Redirect("/member/login");
            }


            return Redirect("/member/signup");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Id, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect("/home/index");
            }

            return Redirect("/member/login");
        }

    }
}
