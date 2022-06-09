
namespace Reagentes.Models
{
    public class 用户凭据模型
    {
        [Key]
        public long 注册号 { get; set; }
        public long 用户 { get; set; }
        public long 唯一的试剂序列号 { get; set; }

        [Required]
        public DateTime 凭据更新日期 { get; set; }
    }
}
