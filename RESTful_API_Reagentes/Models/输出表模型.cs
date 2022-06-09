
namespace Reagentes.Models
{
    [Keyless]
    public class 输出表模型
    {

        //public long 输出号 { get; set; }
        [Required]
        public long 用户 { get; set; }
        public long 订单号 { get; set; }
        [Required]
        public DateTime 出发日期 { get; set; }
        public double 出口总瓶重 { get; set; }
    }
}
