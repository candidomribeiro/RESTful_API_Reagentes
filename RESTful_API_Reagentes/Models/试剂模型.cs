
namespace Reagentes.Models
{
    public class 试剂模型
    {
        [Key]
        public long  唯一的试剂序列号 { get; set; }

        [Required]
        public string 试剂名称 { get; set; }

        [Required]
        public string 试剂的化学功能 { get; set; }

        [Required]
        public string 试剂类别 { get; set; }

        [Required]
        public long 报废多少天 { get; set; }

        public string 注释 { get; set; }
    }
}
