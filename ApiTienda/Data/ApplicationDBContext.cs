using ApiTienda.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiTienda.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (!dbCreator.CanConnect())
                {dbCreator.Create();
                }
                if (!dbCreator.HasTables())
                {Database.Migrate();
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }

                    
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
