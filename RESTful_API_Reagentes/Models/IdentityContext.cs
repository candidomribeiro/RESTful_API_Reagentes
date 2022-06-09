using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Reagentes.Models
{
    public class IdentityContext : IdentityDbContext //<IdentityUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        public DbSet<视野角色模型> View_usr { get; set; }
    }
}
