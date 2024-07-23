using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class VentaDTO
    {

        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Subtotal { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }
        public ICollection<DetalleVentaDTO> Productos { get; set; } = new List<DetalleVentaDTO>();
    }
}
