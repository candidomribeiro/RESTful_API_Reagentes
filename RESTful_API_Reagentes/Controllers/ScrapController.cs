
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class ScrapController : Controller
    {
        private DataContext Context { get; }
        private readonly IHttpContextAccessor httpContextAccessor;
        public ScrapController(IHttpContextAccessor httpContext, DataContext ctx)
        {
            httpContextAccessor = httpContext;
            Context = ctx;
        }
        public class 报废标记
        {
            public long 唯一的试剂序列号 { get; set; }
            public long 瓶号 { get; set; }
            public double 数量 { get; set; }
            public string 注释 { get; set; }
        }

        //private string GetUserName()
        //{
        //    return httpContextAccessor.HttpContext.User.Identity.Name;
        //}

        [HttpPost]
        public async Task<IActionResult> 报废([FromBody] 报废标记 废)
        {
            报废标记模型 报废 = new();

            报废.唯一的试剂序列号 = 废.唯一的试剂序列号;
            报废.瓶号 = 废.瓶号;
            报废.数量 = 废.数量;
            报废.日期 = DateTime.Now;
            报废.用户名 = httpContextAccessor.HttpContext.User.Identity.Name;
            报废.注释 = 废.注释;

            var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.报废标记(唯一的试剂序列号,瓶号,数量,日期,用户名,注释) values({报废.唯一的试剂序列号},{报废.瓶号},{报废.数量},{报废.日期},{报废.用户名},{报废.注释})");
            return Ok(ret);
        }

        [HttpGet]
        public async Task<List<报废标记模型>> 报废()
        {
            return await Context.报废标记.ToListAsync<报废标记模型>();
        }

        [HttpDelete]
        public async Task<IActionResult> 删除([FromBody] List<long> 瓶号)
        {
            long t = 0;
            foreach(long l in 瓶号)
            {
               t += await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.报废标记 where 瓶号={l}");
            }
            return Ok(t);
        }
    }
}
