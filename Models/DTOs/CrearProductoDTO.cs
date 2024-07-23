using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class CrearProductoDTO
    {
        [Required(ErrorMessage = "El nombre no puede ser nulo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción no puede ser nula")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El precio unitario no puede ser nulo")]
        public double PrecioUnitario { get; set; }
        [Required(ErrorMessage = "El stock no puede ser nulo")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La categoria no puede ser nula")]
        public long categoriaId { get; set; }

    }
}
