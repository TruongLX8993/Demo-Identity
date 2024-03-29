using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;

namespace WebTest.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase {
        private RoleManager<AppRole> manager;

        public ActionResult Test() {
            return Ok();
        }
        public RolesController(RoleManager<AppRole> manager) {
            this.manager = manager;
        }

        [Authorize()]
        [Route("")]
        public async Task<ActionResult<IList<AppRole>>> GetAll() {
            if (!await manager.RoleExistsAsync("TestRole")) {
                var role = new AppRole {Name = "TestRole", Description = "Test Role"};
                var result = await manager.CreateAsync(role);
            }

            return manager.Roles.ToList();
        }
    }
}
