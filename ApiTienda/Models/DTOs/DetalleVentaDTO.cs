using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTienda.Models.DTOs
{
    public class DetalleVentaDTO
    {
        public long ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal { get; set; }
    }
}
