
namespace Reagentes.Models
{
    public class 试剂用户模型
    {
        [Key]
        public long 用户 { get; set; }
        public long? 工号 { get; set; }

        public string 用户名 { get; set; }

        [Required]
        public string 名字 { get; set; }
        public string 角色 { get; set; }
        [Required]
        public string 身份证号 { get; set; }
        [Required]
        public string 邮件 { get; set; }
        public string 电话号码 { get; set; }
        [Required]
        public DateTime 注册日期 { get; set; }

    }
}
