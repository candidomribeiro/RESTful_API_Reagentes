
namespace Reagentes.Models
{
    [Keyless]
    public class 试剂采购频率模型
    {
        public long 数 { get; set; }
        public long 唯一的试剂序列号 { get; set; }
        public string 试剂名称 { get; set; }
        public string 试剂类别 { get; set; }
        public double 一共 { get; set; }
        public DateTime 购买日期时间 { get; set; }
    }
}
