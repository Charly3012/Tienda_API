using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTienda.Models.DTOs
{
    public class ProductoDTO
    {

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public long categoriaId { get; set; }
        public Categoria categoria { get; set; }


    }
}
