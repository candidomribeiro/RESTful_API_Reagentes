
namespace Reagentes.Models
{
    public class RFID标签模型
    {
        [Key]
        public long RFID标签编号 { get; set; }
        public long 唯一的试剂序列号 { get; set; }

        [Required]
        public DateTime 创立日期 { get; set; }
    }
}
