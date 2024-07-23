using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTienda.Models.DTOs
{
    public class VentasVariasDTO
    {

        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Subtotal { get; set; }
        public double Iva { get; set; }
        public double Total { get; set; }

    }
}
