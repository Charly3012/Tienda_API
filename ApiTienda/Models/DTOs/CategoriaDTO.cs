using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class CategoriaDTO
    {
        public long Id { get; set; }

        [MaxLength(100, ErrorMessage = "El número maximo de caracteres es 100")]
        public string Nombre { get; set; }

        [MaxLength(500, ErrorMessage = "El número maximo de caracteres es 500")]
        public string Descripcion { get; set; }

    }
}
