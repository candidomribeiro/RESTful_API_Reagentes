
namespace Reagentes.Models
{
    public class 报废模型
    {
        [Key]
        public long 瓶号 { get; set; }
        public long 唯一的试剂序列号 { get; set; }
        public long RFID标签编号 { get; set; }
        public string 瓶型 { get; set; }
        public double 瓶子容量 { get; set; }
        public double 皮重 { get; set; }

        [Required]
        public DateTime 瓶子丢弃日期 { get; set; }
    }
}
