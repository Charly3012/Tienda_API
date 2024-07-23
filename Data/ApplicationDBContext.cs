using ApiTienda.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { 
                    
        }


        //Recordatorio: Poner las clases que queremos mapear aquí 
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Productos)
                .WithOne(dv => dv.venta)
                .HasForeignKey(dv => dv.VentaId);
        }
    }
}
