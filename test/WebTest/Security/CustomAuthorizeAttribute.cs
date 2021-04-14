using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebTest.Security {
    public class CustomAuthorizeAttribute : ActionFilterAttribute {
        private string[] _permission;

        public CustomAuthorizeAttribute(string permission) {
            _permission = permission?.Split(";");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            var user = filterContext.HttpContext.User;
            if (user == null)
                filterContext.Result = new RedirectResult("/Account/Login");

            var userPermissions =
                filterContext.HttpContext.User.Claims.Where(claim => claim.Type == ClaimExtensions.PermissionsType)
                    .Select(claim => claim.Value)
                    .ToList();

            var hasPer = userPermissions.Where(per => _permission.Contains(per)).Count() > 0;
            if (hasPer)
                return;
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
