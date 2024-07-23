using ApiTienda.Models;

namespace ApiTienda.Repository.IRepository
{
    public interface IVentaRepository
    {
        ICollection<Venta> GetVentas();
        Venta GetVenta(long ventaId);
        bool ExisteVenta(long id);
        bool CrearVenta(Venta venta);
        bool ActualizarVenta(Venta venta);
        bool BorrarVenta(Venta venta);
        bool Guardar();
    }
}
