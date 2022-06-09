
namespace Reagentes.Models
{
    [Keyless]
    public class 输回表模型
    {
        // public long 输回号 { get; set; }
        public long 用户 { get; set; }
        public long 订单号 { get; set; }

        [Required]
        public DateTime 归期 { get; set; }
        public double 瓶子总重 { get; set; }
    }
}
