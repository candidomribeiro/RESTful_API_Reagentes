
namespace Reagentes.Models
{
    public class 瓶子模型 
    {
        [Key]
        public long 瓶号 { get; set; }
        public long RFID标签编号 { get; set; }

        [Required]
        public DateTime 瓶子创立日期 { get; set; }
        [Required]
        public DateTime 报废日期 { get; set; }
        public string 瓶型 { get; set; }
        public double 瓶子容量 { get; set; }
        public double 皮重 { get; set; }
        public double 瓶子的总重量 { get; set; }
    }
}
