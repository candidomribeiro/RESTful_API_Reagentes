
namespace Reagentes.Models
{
    public class 仓库地方模型
    {
        [Key]
        public long 瓶号 { get; set; }
        public long 仓库编号 { get; set; }
        public long 柜号 { get; set; }
        public long 地位 { get; set; }
        public string 其他信息 { get; set; }
    }
}
