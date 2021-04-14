using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Models;
using WebTest.Security;

namespace WebTest.Controllers {
    public class HomeController : Controller {

        [Authorize(Roles = "Admin")]
        public IActionResult Index() {
            return View();
        }

        [CustomAuthorize("view-about")]
        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        [CustomAuthorize("view-contract")]
        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
