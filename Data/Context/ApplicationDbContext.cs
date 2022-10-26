using Microsoft.EntityFrameworkCore;
using ExercicioAPIStella.Data.Entities;

namespace ExercicioAPIStella.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
