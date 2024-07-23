using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models
{
    public class Venta
    {

        [Key]
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Subtotal { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }

        //Relación con DetalleVenta
        public ICollection<DetalleVenta> Productos { get; set; } = new List<DetalleVenta>();
    }
}
