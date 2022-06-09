
namespace Reagentes.Models
{
    [Keyless]
    public class 产品使用频率模型
    {
        public long 唯一的试剂序列号 { get; set; }
        public string 试剂名称 { get; set; }
        public double 数量 { get; set; }
        public long 天 { get; set; }
        public double 数量除天 { get; set; }
    }
}
