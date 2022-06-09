
namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户")]
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private DataContext Context { get; }
        public OrderController(IHttpContextAccessor httpContext, DataContext ctx)
        {
            httpContextAccessor = httpContext;
            Context = ctx;
        }
        public class 参数
        {
            public long 操作类型 { get; set; }
            public string 开始日期 { get; set; }
            public string 结束日期 { get; set; }
            public long 瓶号 { get; set; }
            public string 注释 { get; set; }
        }

        public class 要求赞同
        {
            public long 订单号 { get; set; }
            public long 赞同 { get; set; }
            public string 管理注释 { get; set; }
        }

        private string GetUserName()
        {
            return httpContextAccessor.HttpContext.User.Identity.Name;
        }

        private IEnumerable<long> GetUserNumber(string username)
        {
            return Context.试剂用户.Where(q => q.用户名 == username).Select(q => q.用户);
        }

        [HttpGet]
        public async Task<List<视野瓶可用模型>> 可以选择()
        {
            long 用户 = GetUserNumber(GetUserName()).First();

            var 凭据 = await Context.用户凭据.Where(q => q.用户 == 用户).Select(q => q.唯一的试剂序列号).ToArrayAsync();

            return await Context.视野瓶可用.Where(q => 凭据.Contains(q.唯一的试剂序列号)).ToListAsync<视野瓶可用模型>();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> 要求([FromBody] 参数 请求)
        {
            long 用户 = GetUserNumber(GetUserName()).First();

            DateTime 开始日期 = DateTime.Parse(请求.开始日期);
            DateTime 结束日期 = DateTime.Parse(请求.结束日期);

            try
            {
                var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序要求 ({用户},{请求.操作类型},{开始日期},{结束日期},{请求.瓶号},{请求.注释})");
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest("瓶子不可用或未经授权 " + e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = @"用户取消订单")]
        public async Task<IActionResult> 取消([FromBody] long 订单号)
        {
            try
            {
                var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.要求赞同 set 赞同=-2,日期时间={DateTime.Now} where 订单号={订单号}");
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest("失败 " + e.Message);
            }
        }

        /// <summary>
        /// 列出所有用户订单。
        /// 赞同 = 0; 待定
        /// 赞同 = 1; 批准
        /// 赞同 = 2; 应用程序已关闭
        /// 赞同 = 10; 正在进行中
        /// 赞同 = -1; 经理不赞成
        /// 赞同 = -2; 被用户取消
        /// </summary>
        /// <returns></returns>
        [HttpGet("myorders")]
        [SwaggerOperation(Description = @"列出所有用户订单。赞同 = 0 待定; 赞同 = 1 批准; 赞同 = 2 应用程序已关闭; 赞同 = 10 正在进行中; 赞同 = -1 经理不赞成; 赞同 = -2 被用户取消;")]
        public async Task<List<视野用户要求模型>> MyOrders()
        {
            return await Context.视野用户要求.Where(q => q.用户名 == GetUserName()).ToListAsync<视野用户要求模型>();
        }

        /// <summary>
        /// 查阅悬而未决。
        /// 赞同 = 0 ; 待定
        /// </summary>
        /// <returns></returns>
        [HttpGet("manager")]
        [Authorize(Roles = "管理")]
        [SwaggerOperation(Description = @"查阅悬而未决。 赞同 = 0 待定;")]
        public async Task<List<视野用户要求模型>> Qry()
        {
            return await Context.视野用户要求.Where(q => q.赞同 == 0).ToListAsync<视野用户要求模型>();
        }

        /// <summary>
        /// 赞成或谴责。
        /// 赞同 = 0 ; 待定
        /// 赞同 = 1 ; 批准
        /// 赞同 = 2; 应用程序已关闭
        /// 赞同 = 10; 正在进行中
        /// 赞同 = -1; 经理不赞成
        /// 赞同 = -2; 被用户取消
        /// </summary>
        /// <param name="赞同"></param>
        /// <returns></returns>
        [HttpPut("manager")]
        [Authorize(Roles = "管理")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = @"赞成或谴责。 赞同 = 0 待定; 赞同 = 1 批准; 赞同 = 2 应用程序已关闭; 赞同 = 10 正在进行中; 赞同 = -1 经理不赞成; 赞同 = -2 被用户取消;")]
        public async Task<IActionResult> 允许 ([FromBody] 要求赞同 要求)
        {
            要求赞同模型 赞同 = new();
            赞同.订单号       = 要求.订单号;
            赞同.管理名       = GetUserName();
            赞同.管理用户     = GetUserNumber(GetUserName()).First();
            赞同.赞同         = 要求.赞同;
            赞同.日期时间     = DateTime.Now;
            赞同.管理注释     = 要求.管理注释;

            try
            {
                var ret = await Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.要求赞同 set 管理名={赞同.管理名},管理用户={赞同.管理用户},日期时间={赞同.日期时间},管理注释={赞同.管理注释},赞同={赞同.赞同} where 订单号={赞同.订单号}");
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest("失败 " + e.Message);
            }
        }
    }
}
