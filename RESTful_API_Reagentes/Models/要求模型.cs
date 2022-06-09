
namespace Reagentes.Models
{
    public class 要求模型
    {
        [Key]
        public long 订单号 { get; set; }
        public long 用户 { get; set; }

        [Required]
        public DateTime 订单日期 { get; set; }
        public long 操作类型 { get; set; }

        [Required]
        public DateTime 开始日期 { get; set; }
        [Required]
        public DateTime 结束日期 { get; set; }
        public long 瓶号 { get; set; }
        public string 注释 { get; set; }

    }
}
