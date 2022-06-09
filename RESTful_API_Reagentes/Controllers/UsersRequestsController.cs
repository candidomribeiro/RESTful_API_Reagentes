
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/databaseviews/[controller]")]
    [Authorize(Roles = "管理")]
    public class UsersRequestsController : Controller
    {
        private DataContext Context { get; }
        public UsersRequestsController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public async Task<List<视野用户要求模型>> 用户要求()
        {
            return await Context.视野用户要求.ToListAsync<视野用户要求模型>();
        }

        [HttpGet("request/{订单号?}")]
        public async Task<List<视野用户要求模型>> 用户要求(long 订单号)
        {
            return await Context.视野用户要求.Where(q => q.订单号 == 订单号).ToListAsync<视野用户要求模型>();
        }
    }
}
