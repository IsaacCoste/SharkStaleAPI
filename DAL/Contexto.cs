using Microsoft.EntityFrameworkCore;
using SharkStyleApi.Models;

namespace SharkStyleApi.DAL

{
    public class Contexto : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<DetalleCarrito> DetallesCarrito { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<DetalleProducto> DetallesProducto { get; set; }
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Any specific configuration can go here
        }
    }
}
