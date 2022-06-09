using System.Data;

namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DataContext Context;
        private readonly IdentityContext Identity;

        public UsersController(UserManager<IdentityUser> usrManager, DataContext ctx, IdentityContext idt, IHttpContextAccessor httpContext)
        {
            UserManager = usrManager;
            Context = ctx;
            Identity = idt;
            httpContextAccessor = httpContext;
        }

        public class 用户信息
        {
            [Required(ErrorMessage = "工号不能空")]
            public long 工号 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能空")]
            public string 用户名 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "名字不能空")]
            public string 名字 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能空")]
            public string 密码 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "身份证号不能空")]
            public string 身份证号 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "邮件不能空")]
            public string 邮件 { get; set; }
            public string 电话号码 { get; set; }
        }

        public class 更新用户信息
        {
            [Required(ErrorMessage = "工号不能空")]
            public long 工号 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能空")]
            public string 用户名 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "名字不能空")]
            public string 名字 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "身份证号不能空")]
            public string 身份证号 { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "邮件不能空")]
            public string 邮件 { get; set; }
            public string 电话号码 { get; set; }
        }

        public class 用户角色
        {
            [Required]
            public string 用户名 { get; set; }
            [Required]
            public string 角色 { get; set; }
        }

        public class 用户名字
        {
            [Required]
            public string 用户名 { get; set; }
            [Required]
            public string 新密码 { get; set; }
        }

        public class 用户密码
        {
            [Required]
            public string 用户名 { get; set; }
            [Required]
            public string 老密码 { get; set; }
            [Required]
            public string 新密码 { get; set; }
        }

        public class 用户删除
        {
            [Required]
            public string 用户名 { get; set; }
        }

        [HttpGet]
        public async Task<List<试剂用户模型>> ListCurrentUser()
        {
            var 名字 = httpContextAccessor.HttpContext.User.Identity.Name;
            return await Context.试剂用户.Where(q => q.用户名 == 名字).ToListAsync<试剂用户模型>();
        }

        [HttpGet("all")]
        [Authorize(Roles = "管理")]
        public async Task<List<试剂用户模型>> ListAllUsers()
        {
            return await Context.试剂用户.OrderBy(q => q.用户名).ToListAsync<试剂用户模型>();
        }

        [HttpGet("activeusers")]
        [Authorize(Roles = "管理")]
        public async Task<List<试剂用户模型>> ListActiveUsers()
        {
            return await Context.试剂用户.Where(q => q.用户名 != null).OrderBy(q => q.用户名).ToListAsync<试剂用户模型>();
        }

        [HttpPost]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OnPostAsync([FromBody] 用户信息 消息)
        {
            IdentityUser user = new();
            user.UserName = 消息.用户名;
            user.Email = 消息.邮件;
            user.PhoneNumber = 消息.电话号码;

            if (ModelState.IsValid)
            {
                try { await UserManager.CreateAsync(user, 消息.密码); }
                catch (Exception e) { return BadRequest("UserManager.CreateAsync(user, userinfo.Password); 失败 !! " + e.Message); }

                试剂用户模型 用户 = new();
                用户.用户名 = 消息.用户名;
                用户.工号 = 消息.工号;
                用户.名字 = 消息.名字;
                用户.身份证号 = 消息.身份证号;
                用户.邮件 = 消息.邮件;
                用户.电话号码 = 消息.电话号码;
                用户.注册日期 = DateTime.Now;

                try
                {
                    var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.试剂用户(用户,工号,用户名,名字,角色,身份证号,邮件,电话号码,注册日期) values(null,{用户.工号},{用户.用户名},{用户.名字},null,{用户.身份证号},{用户.邮件},{用户.电话号码},{用户.注册日期})");
                    return Ok(ret);
                }
                catch (Exception e)
                {
                    return BadRequest("UsersController SQL insert 失败 !! " + e.Message);
                }
            }
            return BadRequest("ModelState");
        }


        [HttpPut]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OnPutAsyncUpdate([FromBody] 更新用户信息 用户)
        {
            IdentityUser user = await UserManager.FindByNameAsync(用户.用户名);

            user.Email = 用户.邮件 ?? user.Email;
            user.PhoneNumber = 用户.电话号码 ?? user.PhoneNumber;

            try
            {
                await UserManager.UpdateAsync(user);
                var ret = Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.试剂用户 set 工号={用户.工号},名字={用户.名字},身份证号={用户.身份证号},邮件={用户.邮件},电话号码={用户.电话号码} where 用户名={用户.用户名}");
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePass([FromBody] 用户密码 密码)
        {
            IdentityUser user = await UserManager.FindByNameAsync(密码.用户名);
            var r = await UserManager.ChangePasswordAsync(user, 密码.老密码, 密码.新密码);
            if (!r.Succeeded)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut("forgotpassword")]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemovePassword([FromBody] 用户名字 用户)
        {
            IdentityUser user = await UserManager.FindByNameAsync(用户.用户名);
            UserManager.RemovePasswordAsync(user).Wait();

            var ret = await UserManager.AddPasswordAsync(user, 用户.新密码);
            return Ok(ret);
        }

        [HttpPut("grant")]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OnPostAsyncGrant([FromBody] 用户角色 用户)
        {
            IdentityUser user = await UserManager.FindByNameAsync(用户.用户名);
            try
            {
                List<string> role = new(1); 
                role.Add(用户.角色);
                await UserManager.AddToRolesAsync(user, role);
                await UserManager.UpdateAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var ret = await UpdateRoleInformation(用户);

            return Ok(ret);
        }

        [HttpPut("revoke")]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OnPostAsyncRevoke([FromBody] 用户角色 用户)
        {
            IdentityUser user = await UserManager.FindByNameAsync(用户.用户名);
            try
            {
                List<string> role = new(1);
                role.Add(用户.角色);
                await UserManager.RemoveFromRolesAsync(user, role);
                await UserManager.UpdateAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var ret = await UpdateRoleInformation(用户);

            return Ok(ret);
        }

        [HttpDelete]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> OnPostAsyncDel([FromBody] 用户删除 用户)
        {
            IdentityUser user = new();
            user.UserName = 用户.用户名;

            user = await UserManager.FindByNameAsync(user.UserName);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
                await Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序用户删除 ({user.UserName})");
            }
            return Ok();
        }

        private async Task<int> UpdateRoleInformation(用户角色 用户)
        {
            try
            {
                var roles = await Identity.View_usr.Where(q => q.username == 用户.用户名).Select(s => s.role).ToArrayAsync();
                var allroles = String.Join(",", roles);
                return await Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.试剂用户 set 角色={allroles} where 用户名={用户.用户名}");
            }
            catch
            {
                return -1;
            }
        }
    }
}
