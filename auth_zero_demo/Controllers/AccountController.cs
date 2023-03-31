using auth_zero_demo.Models;
using Auth0.AspNetCore.Authentication;
using System.Security.Claims;

namespace auth_zero_demo.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl="/")
        {
            var authenticationProps = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProps);
        }
        [Authorize]
        public async Task Logout()
        {
            var authenticationProps = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index","Home"))
                .Build();
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProps);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [Authorize]
        public IActionResult UserProfile()
        {
            return View(new userProfile()
            {
                Name = User.Identity.Name,
                Email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                Avatar = User.FindFirst(c => c.Type == "picture")?.Value
            });
        }
    }
}
