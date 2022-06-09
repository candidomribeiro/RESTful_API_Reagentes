
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,技术员")]
    public class AbnormalBehaviourController : Controller
    { 
        private DataContext Context { get; }
        public AbnormalBehaviourController(DataContext ctx)
        {
            Context = ctx;
        }

        public class 异常行为
        {
            public long 用户 { get; set; }
            public long 一种例外 { get; set; }
            public long 操作类型 { get; set; }
            public DateTime 日期时间 { get; set; }
        }

        [HttpGet]
        public async Task<List<异常行为模型>> 异常()
        {
            return await Context.异常行为.ToListAsync<异常行为模型>();
        }

        [HttpPost]
        public async Task<IActionResult> 异常([FromBody] 异常行为 异)
        {
            var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.异常行为 values (null, {异.用户},{异.一种例外},{异.操作类型},{异.日期时间})");
            return Ok(ret);
        }
    }
}
