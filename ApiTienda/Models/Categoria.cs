using ApiTienda.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }


        public void ActualizarCategoria(CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO.Nombre != null)
            {
                this.Nombre = categoriaDTO.Nombre;
            }
            if (categoriaDTO.Descripcion != null)
            {
                this.Descripcion = categoriaDTO.Descripcion;
            }

        }


    }
}
