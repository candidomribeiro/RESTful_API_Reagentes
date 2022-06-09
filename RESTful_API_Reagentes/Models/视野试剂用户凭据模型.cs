
namespace Reagentes.Models
{
    [Keyless]
    public class 视野试剂用户凭据模型
    {
        public DateTime? 凭据更新日期 { get; set; }
        public long? 唯一的试剂序列号 { get; set; }
        public DateTime? 注册日期 { get; set; }
        public string 注释 { get; set; }
        public long? 用户 { get; set; }
        public long? 工号 { get; set; }
        public string 用户名 { get; set; }
        public string 试剂名称 { get; set; }
        public string 试剂的化学功能 { get; set; }
        public string 试剂类别 { get; set; }
    }
}
