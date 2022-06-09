using System.Data;
using ClosedXML.Excel;

namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理")]
    public class LogExportController : Controller
    {
        private readonly DataContext Context;
        public LogExportController(DataContext ctx)
        {
            Context = ctx;
        }
        public class 档案
        {
            private string _档案名;
            public string 档案名 { get { return _档案名; } set { _档案名 = value + ".xlsx"; } }
            public string 工作表 { get; set; }
        }

        [HttpPost("activeusers")]
        public async Task<IActionResult> ExportUsersData([FromBody] 档案 文件)
        {
            DataTable dt = new(文件.工作表);
            dt.Columns.AddRange(
                new DataColumn[7]
                {
                    new DataColumn("工号"),
                    new DataColumn("用户名"),
                    new DataColumn("名字"),
                    new DataColumn("身份证号"),
                    new DataColumn("邮件"),
                    new DataColumn("电话号码"),
                    new DataColumn("注册日期")
                });

            var 用户 = from 用 in Context.试剂用户 where 用.用户名 != null select 用;

            foreach (var 户 in 用户)
            {
                dt.Rows.Add(户.工号, 户.用户名, 户.名字, 户.身份证号, 户.邮件, 户.电话号码, 户.注册日期);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return await Task.Run(() => File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 文件.档案名));
        }

        [HttpPost("logdata")]
        public async Task<IActionResult> ExportLog([FromBody] 档案 文件)
        {
            DataTable dt = new(文件.工作表);
            dt.Columns.AddRange(
                new DataColumn[10]
                {
                    new DataColumn("登录"),
                    new DataColumn("名字"),
                    new DataColumn("用户名"),
                    new DataColumn("电话号码"),
                    new DataColumn("IP地址"),
                    new DataColumn("国家"),
                    new DataColumn("城市"),
                    new DataColumn("操作软件"),
                    new DataColumn("浏览器"),
                    new DataColumn("时间")
                });

            var 日志 = from 志 in Context.视野日志 select 志;

            foreach (var 日 in 日志)
            {
                dt.Rows.Add(日.登录, 日.名字, 日.用户名, 日.电话号码, 日.IP地址, 日.国家, 日.城市, 日.操作软件, 日.浏览器, 日.时间);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new();
            wb.SaveAs(stream);

            return await Task.Run(() => File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 文件.档案名));
        }
    }
}
