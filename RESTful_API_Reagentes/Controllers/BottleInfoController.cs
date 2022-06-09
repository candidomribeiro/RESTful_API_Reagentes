
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/databaseviews/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class BottleInfoController : Controller
    {
        private DataContext Context { get; }
        public BottleInfoController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public async Task <List<视野瓶子信息模型>> 瓶子信息()
        {
            return await Context.视野瓶子信息.ToListAsync<视野瓶子信息模型>();
        }

        [HttpGet("rfid/{rfid?}")]
        public async Task<List<视野瓶子信息模型>> 瓶子信息rfid(long rfid)
        {
            return await Context.视野瓶子信息.Where(q => q.RFID标签编号 == rfid).ToListAsync<视野瓶子信息模型>();
        }

        [HttpGet("bottle/{瓶号?}")]
        public async Task<List<视野瓶子信息模型>> 瓶子信息(long 瓶号)
        {
            return await Context.视野瓶子信息.Where(q => q.瓶号 == 瓶号).ToListAsync<视野瓶子信息模型>();
        }

        [HttpGet("reagentnumber/{试剂号?}")]
        public async Task<List<视野瓶子信息模型>> 瓶子信息号(long 试剂号)
        {
            return await Context.视野瓶子信息.Where(q => q.唯一的试剂序列号 == 试剂号).ToListAsync<视野瓶子信息模型>();
        }
    }
}
