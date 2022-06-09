
namespace Reagentes.Models
{
    [Keyless]
    public class 视野日志模型
    {
        public int? 登录 { get; set; }
        public string 名字 { get; set; }
        public string 用户名 { get; set; }
        public string 电话号码 { get; set; }
        public string IP地址 { get; set; }
        public string 国家 { get; set; }
        public string 城市 { get; set; }
//        public string 机器 { get; set; }
        public string 操作软件 { get; set; }
        public string 浏览器 { get; set; }
        public DateTime? 登录日期 { get; set; }
        public DateTime? 登出日期 { get; set; }
        public TimeSpan? 时间 { get; set; }
    }
}
