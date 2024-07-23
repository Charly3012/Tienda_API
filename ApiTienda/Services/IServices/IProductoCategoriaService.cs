using ApiTienda.Models;
using ApiTienda.Models.DTOs;

namespace ApiTienda.Services.IServices
{
    public interface IProductoCategoriaService
    {
        Categoria AsignarCategoria(long categoriaId);

        Producto ActualizarProducto(Producto producto, ActualizarProductoDTO actualizarProductoDTO);
    }
}
