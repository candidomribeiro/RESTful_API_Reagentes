
namespace Reagentes.Models
{
    [Keyless]
    public class 视野瓶子信息模型 
    {
        public long RFID标签编号 { get; set; }
        public long 仓库柜号 { get; set; }
        public long 仓库编号 { get; set; }
        public string 其他信息 { get; set; }
        public long 唯一的试剂序列号 { get; set; }
        public long 地位 { get; set; }
        public long 报废多少天 { get; set; }
        public DateTime 报废日期 { get; set; }
        public long 瓶号 { get; set; }
        public string 瓶型 { get; set; }
        public DateTime 瓶子创立日期 { get; set; }
        public double 瓶子容量 { get; set; }
        public double 瓶子的总重量 { get; set; }
        public double 皮重 { get; set; }
        public string 试剂名称 { get; set; }
        public string 试剂注释 { get; set; }
        public string 试剂的化学功能 { get; set; }
        public string 试剂类别 { get; set; }
    }
}
