
namespace Reagentes.Models
{
    public class 角色模型
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
