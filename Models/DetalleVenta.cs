using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTienda.Models
{
    public class DetalleVenta
    {
        [Key]
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public string NombreProducto    { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario   { get; set; }
        public double Subtotal { get; set; }

        //Relación con venta
        public long VentaId { get; set; }
        [ForeignKey("VentaId")]
        public Venta venta { get; set; }
    }
}
