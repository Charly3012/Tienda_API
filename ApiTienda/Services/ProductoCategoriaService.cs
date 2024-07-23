using ApiTienda.Exceptions;
using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using ApiTienda.Services.IServices;

namespace ApiTienda.Services
{
    public class ProductoCategoriaService : IProductoCategoriaService
    {
        private readonly ICategoriaRepository _ctRepo;

        public ProductoCategoriaService(ICategoriaRepository ctRepo)
        {
            _ctRepo = ctRepo;
        }


        public Categoria AsignarCategoria(long categoriaId)
        {
            var categoria = _ctRepo.GetCategoria(categoriaId);
            if (categoria == null)
            {
                throw new CategoriaNotFoundException("Categoria no encontrada");
            }

            return categoria;
        }

        public Producto ActualizarProducto(Producto producto, ActualizarProductoDTO actualizarProductoDTO)
        {
            if(actualizarProductoDTO.Nombre != null)
            {
                producto.Nombre = actualizarProductoDTO.Nombre;
            }
            if(actualizarProductoDTO.Descripcion != null)
            {
                producto.Descripcion = actualizarProductoDTO.Descripcion;
            }
            if (actualizarProductoDTO.PrecioUnitario.HasValue)
            {
                producto.PrecioUnitario = actualizarProductoDTO.PrecioUnitario.Value;
            }
            if (actualizarProductoDTO.Stock.HasValue)
            {
                producto.Stock = actualizarProductoDTO.Stock.Value;
            }
            if(actualizarProductoDTO.categoriaId.HasValue)
            {
                producto.categoriaId = actualizarProductoDTO.categoriaId.Value;
                producto.categoria = AsignarCategoria(producto.categoriaId);
            }

            return producto;
        }
    }
}
