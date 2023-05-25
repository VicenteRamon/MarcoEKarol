using Microsoft.EntityFrameworkCore;

namespace MarcoEKarol.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
        }

        public DbSet<ListaDePresentes> ListaDePresentes { get;set; }

    }
}
