
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,技术员")]
    public class AcquisitionsController : Controller
    {
        private DataContext Context { get; }
        public AcquisitionsController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 试剂采购
        {
            private DateTime 购买日期时间_;
            public long 唯一的试剂序列号 { get; set; }
            public double 购买数量 { get; set; }
            public DateTime 购买日期时间 
            { 
                get { return 购买日期时间_; } 
                set { 购买日期时间_ = DateTime.Parse(value.ToString()); }
            }
        }

        [HttpGet]
        public async Task<List<试剂采购模型>> 显示采购()
        {
            return await Context.试剂采购.ToListAsync<试剂采购模型>();
        }

        [HttpPost]
        public async Task<IActionResult> 采购([FromBody] List<试剂采购> 采购)
        {
            foreach (试剂采购 购 in 采购)
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.试剂采购 values (null,{购.唯一的试剂序列号},{购.购买数量},{购.购买日期时间})");
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> 删除([FromBody] List<long> 采购号)
        {
            foreach (long 号 in 采购号)
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.试剂采购 where 采购号={号}");
            }
            return Ok();
        }
    }
}
