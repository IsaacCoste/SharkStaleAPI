using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace SharkStyleApi.DAL

{
    public class Contexto : DbContext
    {
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Carritos> Carritos { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    }
}
