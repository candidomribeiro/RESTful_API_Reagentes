
namespace Reagentes.Models
{
    public class 异常行为模型
    {
        [Key]
        public long? 注册号 { get; set; }
        [Required]
        public long 用户 { get; set; }
        [Required]
        public long 一种例外 { get; set; }
        [Required]
        public long 操作类型 { get; set; }
        [Required]
        public DateTime 日期时间 { get; set; }
    }
}
