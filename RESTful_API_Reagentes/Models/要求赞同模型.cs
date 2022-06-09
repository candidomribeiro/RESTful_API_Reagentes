
namespace Reagentes.Models
{
    [Keyless]
    public class 要求赞同模型
    {
        public long 订单号 { get; set; }
        public long 管理用户 { get; set; }
        public string 管理名 { get; set; }
        public long 赞同 { get; set; }

        [Required]
        public DateTime 日期时间 { get; set; }
        public string 管理注释 { get; set; }
    }
}
