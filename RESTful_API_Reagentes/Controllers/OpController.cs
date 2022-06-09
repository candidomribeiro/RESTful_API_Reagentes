
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class OpController : Controller
    {
        private DataContext Context { get; }
        private readonly IHttpContextAccessor httpContextAccessor;

        public OpController(IHttpContextAccessor httpContext, DataContext ctx)
        {
            httpContextAccessor = httpContext;
            Context = ctx;
        }

        private string GetUserName()
        {
            return httpContextAccessor.HttpContext.User.Identity.Name;
        }

        [HttpGet]
        public async Task<List<视野用户要求模型>> Get()
        {
            string username = GetUserName();
            return await Context.视野用户要求.Where(q => q.用户名 == username).OrderByDescending(x => x.订单日期).ToListAsync<视野用户要求模型>();
        }

        [HttpGet("user")]
        public async Task<List<视野用户要求模型>> AdmGet([FromQuery] string name)
        {
            return await Context.视野用户要求.Where(q => q.用户名 == name).OrderByDescending(x => x.订单日期).ToListAsync<视野用户要求模型>();
        }
    }
}
