
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/databaseviews/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class QuantityController : Controller
    {
        private DataContext Context { get; }
        public QuantityController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public async Task<List<视野试剂数量模型>> 试剂数量()
        {
            return await Context.视野试剂数量.ToListAsync<视野试剂数量模型>();
        }

        [HttpGet("serial/{号?}")]
        public async Task<List<视野试剂数量模型>> 试剂数量(long 号)
        {
            return await Context.视野试剂数量.Where(q => q.唯一的试剂序列号 == 号).ToListAsync<视野试剂数量模型>();
        }
    }
}
