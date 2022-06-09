
namespace Reagentes.Models
{
    [Keyless]
    public class 报废标记模型
    {
        public long 唯一的试剂序列号 { get; set; }
        public long 瓶号 { get; set; }
        public double 数量 { get; set; }
        [Required]
        public DateTime 日期 { get; set; }
        //[Required]
        //public long 用户 { get; set; }
        [Required]
        public string 用户名 { get; set; }
        public string 注释 { get; set; }
    }
}
