
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("config/[controller]")]
    [Authorize(Roles = "管理")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public struct 角色
        {
            // public string Userid { get; set; }
            // public string Roleid { get; set; }
            public string 角色名 { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OnPostAsync([FromBody] 角色 指)
        {
            IdentityRole 角 = new();
            角.Name = 指.角色名;

            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(角);
                if (result.Succeeded)
                {
                    return Ok();
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> OnDeleteAsync([FromBody] 角色 指)
        {
            // string name = roleptr.Roleid;
            IdentityRole 角 = await RoleManager.FindByNameAsync(指.角色名);
            var ret = await RoleManager.DeleteAsync(角);
            return Ok(ret);
        }
    }
}
