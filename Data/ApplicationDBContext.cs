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
    }
}
