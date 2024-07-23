using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTienda.Models.DTOs
{
    public class ActualizarProductoDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio")]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double? PrecioUnitario { get; set; }
        public int? Stock { get; set; }
        public long? categoriaId { get; set; }

    }
}
