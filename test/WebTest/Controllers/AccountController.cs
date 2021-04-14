using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;
using WebTest.Security;

namespace WebTest.Controllers {
    public class AccountController : Controller {
        private UserManager<AppUser> userMgr;
        RoleManager<AppRole> roleMgr;

        public AccountController(
            UserManager<AppUser> userMgr,
            RoleManager<AppRole> roleMgr
        ) {
            this.userMgr = userMgr;
            this.roleMgr = roleMgr;
        }

        public async Task<ActionResult> LogIn() {
            var authenticationProperties = new AuthenticationProperties() {RedirectUri = "/Account/Login/"};
            var subject =
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "truonglxsds"),
                        new Claim(ClaimExtensions.PermissionsType, "view-about")
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(subject), authenticationProperties);
            return Content("thanh cong");
        }

        public async Task<ActionResult> AccessDenied() {
            return Content("Không có quyền truy cập");
        }
    }
}
