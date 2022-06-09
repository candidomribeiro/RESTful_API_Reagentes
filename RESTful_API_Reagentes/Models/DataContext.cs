
namespace Reagentes.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<试剂模型> 试剂 { get; set; }  // a variável tem que ter o nome da tabela no banco de dados
        public DbSet<试剂用户模型> 试剂用户 { get; set; }
        public DbSet<输出表模型> 输出表 { get; set; }
        public DbSet<输回表模型> 输回表 { get; set; }
        public DbSet<异常行为模型> 异常行为 { get; set; }
        public DbSet<RFID标签模型> RFID标签 { get; set; }
        public DbSet<用户凭据模型> 用户凭据 { get; set; }
        public DbSet<瓶子模型> 瓶子 { get; set; }
        public DbSet<要求模型> 要求 { get; set; }
        public DbSet<要求赞同模型> 要求赞同 { get; set; }
        public DbSet<仓库地方模型> 仓库地方 { get; set; }
        public DbSet<报废模型> 报废 { get; set; }
        public DbSet<试剂采购模型> 试剂采购 { get; set; }
        public DbSet<RFID标签报废模型> RFID标签报废 { get; set; }
        public DbSet<产品使用频率模型> 产品使用频率 { get; set; }
        public DbSet<试剂使用周期分析模型> 试剂使用周期分析 { get; set; }
        public DbSet<试剂采购频率模型> 试剂采购频率 { get; set; }
        public DbSet<报废标记模型> 报废标记 { get; set; }
        public DbSet<仓库经理模型> 仓库经理 { get; set; }
        public DbSet<产品操作记录模型> 产品操作记录 { get; set; }
 
        // ***************  views ****************
        public DbSet<视野瓶子信息模型> 视野瓶子信息 { get; set; }
        public DbSet<视野试剂数量模型> 视野试剂数量 { get; set; }
        public DbSet<视野试剂用户凭据模型> 视野试剂用户凭据 { get; set; }
        public DbSet<视野用户要求模型> 视野用户要求 { get; set; }
        public DbSet<视野瓶可用模型> 视野瓶可用 { get; set; }
        public DbSet<视野试剂输入输出模型> 视野试剂输入输出 { get; set; }
        public DbSet<视野日志模型> 视野日志 { get; set; }

        // public DbSet<MTeste> Teste { get; set; }
    }
}
