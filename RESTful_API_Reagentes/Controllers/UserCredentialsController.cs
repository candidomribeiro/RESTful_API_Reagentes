
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/databaseviews/[controller]")]
    [Authorize(Roles = "管理")]
    public class UserCredentialsController : Controller
    {
        private DataContext Context { get; }
        public UserCredentialsController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<视野试剂用户凭据模型> 试剂用户凭据()
        {
            return (IAsyncEnumerable<视野试剂用户凭据模型>)Context.视野试剂用户凭据;
        }

        [HttpGet("user/{用户?}")]
        public async Task<List<视野试剂用户凭据模型>> 试剂用户凭据(long 用户)
        {
            return await Context.视野试剂用户凭据.Where(q => q.用户 == 用户).ToListAsync<视野试剂用户凭据模型>();
        }

        [HttpGet("reagentnumber/{试剂号?}")]
        public async Task<List<视野试剂用户凭据模型>> 试剂(long 试剂号)
        {
            return await Context.视野试剂用户凭据.Where(q => q.唯一的试剂序列号 == 试剂号).ToListAsync<视野试剂用户凭据模型>();
        }

        [HttpGet("name/{名字?}")]
        public async Task<List<视野试剂用户凭据模型>> 用户(string 名字)
        {
            return await Context.视野试剂用户凭据.Where(q => q.用户名 == 名字).ToListAsync<视野试剂用户凭据模型>();
        }

    }
}
