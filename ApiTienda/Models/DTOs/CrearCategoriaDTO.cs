using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class CrearCategoriaDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número maximo de caracteres es 100")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [MaxLength(500, ErrorMessage = "El número maximo de caracteres es 500")]
        public string Descripcion { get; set; }

    }
}
