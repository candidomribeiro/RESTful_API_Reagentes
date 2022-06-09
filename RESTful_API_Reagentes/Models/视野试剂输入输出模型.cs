
namespace Reagentes.Models
{
    [Keyless]
    public class 视野试剂输入输出模型
    {
        public long 用户 { get; set; }
        public long? 工号 { get; set; }
        public string 用户名 { get; set; }
        public long 唯一的试剂序列号 { get; set; }
        public string 试剂名称 { get; set; }
        public string 试剂的化学功能 { get; set; }
        public string 试剂类别 { get; set; }
        public long 订单号 { get; set; }
        public long 瓶号 { get; set; }
        public string 注释 { get; set; }
        public DateTime 出发日期 { get; set; }
        public double 出口总瓶重 { get; set; }
        public DateTime 归期 { get; set; }
        public double 瓶子总重 { get; set; }
    }
}
