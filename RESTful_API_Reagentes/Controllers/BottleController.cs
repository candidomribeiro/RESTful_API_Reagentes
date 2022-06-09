
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,技术员")]
    public class BottleController : Controller
    {
        private DataContext Context { get; }
        public BottleController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public async Task<List<瓶子模型>> ListBottle()
        {
            return await Context.瓶子.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBottle([FromBody] 瓶子模型 瓶)
        {
            瓶.瓶子创立日期 = DateTime.Now;

            await Context.瓶子.AddAsync(瓶);
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBottle([FromBody] 瓶子模型 瓶)
        {
            Context.Update(瓶);
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }

        [HttpDelete("{瓶号}")]
        public async Task<IActionResult> DeleteBottle(long 瓶号)
        {
            Context.瓶子.Remove(new 瓶子模型() { 瓶号 = 瓶号 });
            var ret = await Context.SaveChangesAsync();
            return Ok(ret);
        }
    }
}
