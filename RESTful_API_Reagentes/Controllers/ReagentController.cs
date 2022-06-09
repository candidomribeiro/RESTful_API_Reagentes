
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class ReagentController : Controller
    {
        private DataContext Context { get; }
        public ReagentController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 信任状
        {
            public string 用户名 { get; set; }
            public List<long> 用户凭据 { get; set; }
        }

        [HttpGet] 
        public async Task<List<试剂模型>> GetReagents()
        {
            return await Context.试剂.ToListAsync<试剂模型>();
        }

        [HttpGet("{唯一的试剂序列号}")]
        public async Task<试剂模型> GetReagents(long 唯一的试剂序列号)
        {
            return await Context.试剂.FindAsync(唯一的试剂序列号);
        }

        [HttpPost]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> SaveReagents([FromBody]试剂模型 reagents)
        {
            await Context.试剂.AddAsync(reagents);
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }

        [HttpPut]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> UpdateReagents([FromBody] 试剂模型 reagents)
        {
            Context.Update(reagents);
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }

        [HttpDelete("{唯一的试剂序列号}")]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> DeleteReagent(long 唯一的试剂序列号)
        {
            Context.试剂.Remove(new 试剂模型() { 唯一的试剂序列号 = 唯一的试剂序列号 });
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }

        [HttpPost("credentials")]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> AddCredentialToUser([FromBody] 信任状 状)
        {
            long 用户 = Context.试剂用户.Where(q => q.用户名 == 状.用户名).Select(q => q.用户).First();

            foreach(long 号 in 状.用户凭据)
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.用户凭据 values(null, {用户},{号},{DateTime.Now})");
            }
            return Ok();
        }

        [HttpDelete("credentials")]
        [Authorize(Roles = "管理")]
        public async Task<IActionResult> DeleteCredentialFromUser([FromBody] 信任状 状)
        {
            long 用户 = Context.试剂用户.Where(q => q.用户名 == 状.用户名).Select(q => q.用户).First();

            foreach (long 号 in 状.用户凭据)
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.用户凭据 where 用户 = {用户} and 唯一的试剂序列号 = {号}");
            }
            return Ok();
        }
    }
}
