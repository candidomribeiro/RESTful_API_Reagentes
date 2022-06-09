
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理")]
    public class StatisticsController : Controller
    {
        private DataContext Context { get; }
        public StatisticsController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 时间
        {
            public string 开始日期 { get; set; }
            public string 结束日期 { get; set; }
        }

        [HttpPost("usagerate")]
        public async Task<List<产品使用频率模型>> 领用出库率([FromBody] 时间 间)
        {
            Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序格律学({DateTime.Parse(间.开始日期)},{DateTime.Parse(间.结束日期)})").Wait();
            return await Context.产品使用频率.FromSqlInterpolated($"select n as 唯一的试剂序列号, nome as 试剂名称, sum(qtde) as 数量, tempo as 天, round(sum(qtde)/tempo,4) as 数量除天 from reagentes.tmp group by n, nome").ToListAsync<产品使用频率模型>();
        }

        [HttpPost("reagentlifecicle")]
        public async Task<List<试剂使用周期分析模型>> 试剂使用([FromBody] 时间 间)
        {
            Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序周期分析({DateTime.Parse(间.开始日期)}, {DateTime.Parse(间.结束日期)})").Wait();
            return await Context.试剂使用周期分析.FromSqlInterpolated($"select 唯一的试剂序列号, 试剂名称, round(sum(出口总瓶重 - 瓶子总重),4) as 一共, round(min(出口总瓶重 - 瓶子总重),4) as 最小, round(max(出口总瓶重 - 瓶子总重),4) as 最大, 天, round(sum(出口总瓶重 - 瓶子总重) / 天,4) as 一共除天 from reagentes.tmpcicle group by 唯一的试剂序列号, 试剂名称").ToListAsync<试剂使用周期分析模型>();
        }

        [HttpPost("hist")]
        public async Task<List<产品操作记录模型>> 输入输出([FromBody] 时间 间) =>
            await Context.产品操作记录.FromSqlInterpolated($"select 瓶号, 试剂名称, 试剂类别, 用户名, datediff(归期, 出发日期) + 1 as 时间 from reagentes.视野试剂输入输出 where 出发日期 > {DateTime.Parse(间.开始日期)} and 归期 < {DateTime.Parse(间.结束日期)}").ToListAsync<产品操作记录模型>();

        [HttpGet]
        public IAsyncEnumerable<视野试剂输入输出模型> 入出() => Context.视野试剂输入输出;

        [HttpPost("acquisitions")]
        public async Task<List<试剂采购频率模型>> 试剂采购([FromBody] 时间 间) =>
            await Context.试剂采购频率.FromSqlInterpolated($"select count(试剂.唯一的试剂序列号) as 数, 试剂.唯一的试剂序列号, 试剂.试剂名称, 试剂.试剂类别,round(sum(试剂采购.购买数量),4) as 一共, 试剂采购.购买日期时间 from reagentes.试剂 join 试剂采购 on 试剂.唯一的试剂序列号 = 试剂采购.唯一的试剂序列号 where 试剂采购.购买日期时间 between {DateTime.Parse(间.开始日期)} and {DateTime.Parse(间.结束日期)} group by 试剂.唯一的试剂序列号, 试剂.试剂名称, 试剂.试剂类别").ToListAsync<试剂采购频率模型>();
        
    }
}
