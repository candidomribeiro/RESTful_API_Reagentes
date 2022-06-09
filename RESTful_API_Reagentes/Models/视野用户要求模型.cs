
namespace Reagentes.Models
{
    [Keyless]
    public class 视野用户要求模型
    {
        public long? 用户 { get; set; }
        public long? 工号 { get; set; }
        public string 用户名 { get; set; }
        public string 名字 { get; set; }
        public long? 订单号 { get; set; }
        public DateTime? 订单日期 { get; set; }
        public long? 操作类型 { get; set; }
        public DateTime? 开始日期 { get; set; }
        public DateTime? 结束日期 { get; set; }
        public long? 瓶号 { get; set; }
        public string 注释 { get; set; }
        public long? 管理用户 { get; set; }
        public string 管理名 { get; set; }
        public long? 赞同 { get; set; }
        public DateTime? 赞同日期时间 { get; set; }
        public string 管理注释 { get; set; }
    }
}
