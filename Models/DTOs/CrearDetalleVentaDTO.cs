using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class CrearDetalleVentaDTO
    {
        [Required(ErrorMessage = "El Id del producto es requerido")]
        public long ProductoId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser almenos 1")]
        public int Cantidad { get; set; }
    }
}
