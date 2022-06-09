
namespace Reagentes.Models
{
    [Keyless]
    public class 试剂使用周期分析模型
    {
        public long 唯一的试剂序列号 { get; set; }
        public string 试剂名称 { get; set; }
        public double 一共 { get; set; }
        public double 最小 { get; set; }
        public double 最大 { get; set; }
        public long 天 { get; set; }
        public double 一共除天 { get; set; }
    }
}
