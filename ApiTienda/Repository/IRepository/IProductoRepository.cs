using ApiTienda.Models;

namespace ApiTienda.Repository.IRepository
{
    public interface IProductoRepository
    {
        ICollection<Producto> GetProductos();
        Producto GetProducto(long producotId);
        bool ExisteProducto(long id);
        bool ExisteProducto(string nombre);
        bool CrearProducto(Producto producto);
        bool ActualizarProducto(Producto producto);
        bool BorrarProducto(Producto producto);
        bool Guardar();
    }
}
