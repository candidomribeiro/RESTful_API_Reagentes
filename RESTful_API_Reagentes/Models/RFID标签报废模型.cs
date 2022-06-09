
namespace Reagentes.Models
{
    [Keyless]
    public class RFID标签报废模型
    {

     //   public long 报废号 { get; set; }
        public long RFID标签编号 { get; set; }
        public DateTime 标签创立日期 { get; set; }
        public DateTime 标签丢弃日期 { get; set; }
    }
}
