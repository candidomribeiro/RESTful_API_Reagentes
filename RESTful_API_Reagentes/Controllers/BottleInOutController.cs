
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class BottleInOutController : Controller
    {
        private DataContext Context { get; }

        public BottleInOutController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 表格
        {
            public string 用户名 { get; set; }
            public long 瓶号 { get; set; }
            public long 仓库编号 { get; set; }
            public double 出口总瓶重 { get; set; }
            public double 瓶子总重 { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> ListBottle()
        {
            var ret = await Context.视野用户要求.Where(q => q.赞同 == 1).ToListAsync<视野用户要求模型>();
            return Ok(ret);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SelectBottle([FromBody] List<表格> 出)
        {
            foreach (表格 l in 出)
            {
                try
                {
                    await Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序产品输出({l.用户名},{l.瓶号},{l.出口总瓶重})");
                }
                catch(Exception e)
                {
                    return BadRequest("BottleInOutController SelectBottle 失败！！ " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPost("ret")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReturnBottle([FromBody] List<表格> 回)
        {
            foreach (表格 l in 回)
            {
                try
                {
                    await Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序产品退回({l.瓶号},{l.瓶子总重})");
                }
                catch(Exception e)
                {
                    return BadRequest("BottleInOutController ReturnBottle 失败！！ " + e.Message);
                }
            }
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeBottleLocation([FromBody] List<表格> 瓶子)
        {
            foreach(表格 l in 瓶子)
            {
                try
                {
                    await Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.仓库地方 set 仓库编号={l.仓库编号} where 瓶号={l.瓶号}");
                }
                catch(Exception e)
                {
                    return BadRequest("BottleInOutController ChangeBottleLocation 失败！！ " + e.Message);
                }
            }
            return Ok();
        }
    }
}
