using Microsoft.EntityFrameworkCore;
using PersonasAPI.Modelos;

namespace PersonasAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
