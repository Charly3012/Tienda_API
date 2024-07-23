using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ApiTienda.Models
{
    public class Producto
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public double PrecioUnitario { get; set; }
        public int Stock { get; set; }

        //Relación con categoría 
        public long categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria categoria { get; set; }



    }
}
