#pragma warning disable IDE1006

using UAParser;
using Newtonsoft.Json;

namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration Configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly DataContext Context;
        public AccountController(SignInManager<IdentityUser> mgr, IHttpContextAccessor httpContext, DataContext ctx, IConfiguration config)
        {
            signInManager = mgr;
            httpContextAccessor = httpContext;
            Context = ctx;
            Configuration = config;
        }

        public class Credentials
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        private class UserContext
        {
            public IHeaderDictionary Header { get; set; }
            public string RemoteIpAddress { get; set; }
            public string RemotePort { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
        }

        private class GeoLocation
        {
            public string query { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string countryCode { get; set; }
            public string isp { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string org { get; set; }
            public string region { get; set; }
            public string regionName { get; set; }
            public string status { get; set; }
            public string timezone { get; set; }
            public string zip { get; set; }
        }

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromQuery]Credentials creds)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =
                await signInManager.PasswordSignInAsync(creds.Username, creds.Password, false, false);

            if (result.Succeeded)
            {
                _= await LogBook(creds);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginPost([FromBody] Credentials creds)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =
                await signInManager.PasswordSignInAsync(creds.Username, creds.Password, false, false);

            if (result.Succeeded)
            {
                _= await LogBook(creds);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var connection = httpContextAccessor.HttpContext.Connection.Id;
            var 用户名 = httpContextAccessor.HttpContext.User.Identity.Name;
            try
            {
                var 用户 = await Context.试剂用户.Where(q => q.用户名 == 用户名).Select(q => q.用户).FirstAsync();

               // await signInManager.SignOutAsync();
                await Context.Database.ExecuteSqlInterpolatedAsync($"update reagentes.链结id set 登出日期 = now() where 用户 = {用户} and (链结ID = {connection} or 登出日期 is null)");
            }
            catch { }
            finally
            {
                await signInManager.SignOutAsync();
            }
            return Ok();
        }

        private async Task<int> LogBook(Credentials creds)
        {
            UserContext uc = new();
            var connection = httpContextAccessor.HttpContext.Connection.Id;
            uc.Header = httpContextAccessor.HttpContext.Request.Headers;
            uc.RemoteIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            uc.RemotePort = httpContextAccessor.HttpContext.Connection.RemotePort.ToString();

            // Geolocation(ref uc);

            long 用户 = await Context.试剂用户.Where(q => q.用户名 == creds.Username).Select(q => q.用户).FirstAsync();
            var userAgent = uc.Header["User-Agent"];

            static ClientInfo MyUAParser(string ua)
            {
                try
                {
                    var uaParser = Parser.GetDefault();
                    ClientInfo client = uaParser.Parse(ua);
                    return client;
                }
                catch
                {
                    return null;
                }
            }

            string browser, so, device;
            ClientInfo client = MyUAParser(userAgent);
           
            if (client != null)
            {
                browser = client.UA.Family + " " + client.UA.Major + " " + client.UA.Minor;
                so = client.OS.Family + " " + client.OS.Major + " " + client.OS.Minor;
                device = client.Device.Family + " " + client.Device.Brand + " " + client.Device.Model;
            }
            else
            {
                browser = "无名";
                so = "无名";
                device = "无名";
            }

            try
            {
                await Context.Database.ExecuteSqlInterpolatedAsync($"call reagentes.程序登录日志({用户},{uc.RemotePort},{uc.RemoteIpAddress},{device},{so},{browser},{uc.Country},{uc.City},{connection})");
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        //readonly Func<string, ClientInfo> MyUAParser = userAgent => 
        //{
        //    try
        //    {
        //        var uaParser = Parser.GetDefault();
        //        ClientInfo client = uaParser.Parse(userAgent);
        //        return client;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //};

        private void Geolocation(ref UserContext userCtx)
        {
            var ip_api_uri = Configuration["GeolocationURI"] + userCtx.RemoteIpAddress;

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.BaseAddress = new System.Uri(ip_api_uri);
            HttpResponseMessage httpResponse = httpClient.GetAsync(ip_api_uri).GetAwaiter().GetResult();

            if(httpResponse.IsSuccessStatusCode)
            {
                var geolocationInfo = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                GeoLocation geoLocation = JsonConvert.DeserializeObject<GeoLocation>(geolocationInfo);

                userCtx.Country = geoLocation.country;
                userCtx.City = geoLocation.city;
            }
        }
    }
}
