
namespace Reagentes.Models
{
    public class 试剂采购模型
    {
        [Key]
        public long? 采购号 { get; set; }
        [Required]
        public long 唯一的试剂序列号 { get; set; }
        [Required]
        public double 购买数量 { get; set; }
        [Required]
        public DateTime 购买日期时间 { get; set; }
    }
}
