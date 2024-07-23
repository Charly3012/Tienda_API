using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiTienda.Models
{
    public class DetalleVenta
    {
        [Key]
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public string NombreProducto    { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario   { get; set; }
        public double Subtotal { get; set; }

        //Relación con venta
        public long VentaId { get; set; }
        [ForeignKey("VentaId")]
        [JsonIgnore]
        public Venta venta { get; set; }

        public DetalleVenta()
        {
        }

        public DetalleVenta(Producto producto, int cantidad, long ventaId)
        {
            this.ProductoId = producto.Id;
            this.NombreProducto = producto.Nombre;
            this.Cantidad = cantidad;
            this.PrecioUnitario = producto.PrecioUnitario;
            this.Subtotal = producto.PrecioUnitario * cantidad;
            this.VentaId = ventaId;
        }
    }
}
