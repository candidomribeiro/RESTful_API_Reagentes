
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class DisposeController : Controller
    {
        private DataContext Context { get; }
        public DisposeController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<视野瓶子信息模型> PrintList()
        {
            return (IAsyncEnumerable<视野瓶子信息模型>)Context.视野瓶子信息;
        }

        [HttpDelete]
        public async Task<IActionResult> DiposeBottle([FromBody] List<long> toDispose)
        {
            foreach(long id in toDispose)
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.瓶子 where 瓶号={id}");
            }
            return Ok();
        }
    }
}
