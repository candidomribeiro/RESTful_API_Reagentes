
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await Response.WriteAsync("<p>RESTful_Reagentes API</p>");
            return Ok();
        }
    }
}
