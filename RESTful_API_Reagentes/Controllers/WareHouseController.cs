
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,技术员")]
    public class WareHouseController : Controller
    {
        private DataContext Context { get; }
        public WareHouseController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 经理
        {
            public string 用户名 { get; set; }
            public List<long> 仓库编号 { get; set; }
        }

        [HttpGet]
        public async Task <List<视野瓶子信息模型>> L()
        {
            return await Context.视野瓶子信息.ToListAsync<视野瓶子信息模型>();
        }

        [HttpGet("{仓库}")]
        public async Task<List<视野瓶子信息模型>> WareHouseContent(long 仓库)
        {
            return await Context.视野瓶子信息.Where(q => q.仓库编号 == 仓库).ToListAsync<视野瓶子信息模型>();
        }

        [HttpGet("bottle/{瓶号}")]
        public async Task<List<视野瓶子信息模型>> FindBottle(long 瓶号)
        {
            return await Context.视野瓶子信息.Where(q => q.瓶号 == 瓶号).ToListAsync<视野瓶子信息模型>();
        }

        [HttpPost]
        public async Task<IActionResult> SaveWh([FromBody] List<仓库地方模型> 仓库)
        {
            foreach (仓库地方模型 仓 in 仓库)
            {
                await Context.仓库地方.AddAsync(仓);
                await Context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelWh([FromBody] List<long> 瓶号)
        {
            foreach(long 号 in 瓶号)
            {
                try
                {
                    await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.仓库地方 where 瓶号={号}");
                }
                catch(Exception e)
                {
                    return BadRequest("WareHouseController DelWh 失败！！ " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPost("manager")]
        public async Task<IActionResult> AddMananger([FromBody] 经理 清单)
        {
            仓库经理模型 仓 = new();
            仓.注册日期 = DateTime.Now;
            仓.用户 = Context.试剂用户.Where(q => q.用户名 == 清单.用户名).Select(c => c.用户).First();

            foreach(long 号 in 清单.仓库编号)
            {
                仓.仓库编号 = 号;
                await Context.仓库经理.AddAsync(仓);
                await Context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpDelete("manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteManager([FromBody] 经理 清单)
        {
            仓库经理模型 仓 = new();
            仓.用户 = Context.试剂用户.Where(q => q.用户名 == 清单.用户名).Select(c => c.用户).First();

            foreach (long 号 in 清单.仓库编号)
            {
                try
                {
                    await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.仓库经理 where 用户={仓.用户} and 仓库编号={号}");
                }
                catch(Exception e)
                {
                    return BadRequest("WareHouseController DeleteManager 失败 " + e.Message);
                }
            }
            return Ok();
        }
    }
}
