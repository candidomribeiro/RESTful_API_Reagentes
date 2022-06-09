
namespace Reagentes.Models
{
    [Keyless]
    public class 产品操作记录模型
    {
        public long 瓶号 { get; set; }
        public string 试剂名称 { get; set; }
        public string 试剂类别 { get; set; }
        public string 用户名 { get; set; }
        public long 时间 { get; set; }
    }
}
