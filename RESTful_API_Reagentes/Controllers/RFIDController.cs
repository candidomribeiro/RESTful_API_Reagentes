
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,技术员")]
    public class RFIDController : Controller
    {
        private DataContext Context { get; }
        public RFIDController(DataContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public async Task<List<RFID标签模型>> GetRFID()
        {
            return await Context.RFID标签.ToListAsync<RFID标签模型>();
        }

        [HttpPost]
        public async Task<IActionResult> RFID([FromBody] RFID标签模型 rfid)
        {
            rfid.创立日期 = DateTime.Now;

            await Context.RFID标签.AddAsync(rfid);
            var ret = await Context.SaveChangesAsync();

            return Ok(ret);
        }

        [HttpPut]
        public async Task <IActionResult> UpdateRFID([FromBody] RFID标签模型 rfid)
        {
            rfid.创立日期 = DateTime.Now;

            Context.Update(rfid);
            var ret = await Context.SaveChangesAsync();

            return Ok(ret);
        }
    }
}
