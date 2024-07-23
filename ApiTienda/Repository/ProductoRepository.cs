using ApiTienda.Data;
using ApiTienda.Models;
using ApiTienda.Repository.IRepository;
using ApiTienda.Services.IServices;
using Microsoft.Extensions.Options;

namespace ApiTienda.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        //Inyección de dependencias
        private readonly ApplicationDBContext _bd;
        //private readonly IProductoCategoriaService _pcService;
        
        public ProductoRepository(ApplicationDBContext bd)
        {
            _bd = bd;
     
        }


        public bool ActualizarProducto(Producto producto)
        {
            var productoExistente = _bd.Productos.Find(producto.Id);
            if(productoExistente != null)
            {
                _bd.Entry(productoExistente).CurrentValues.SetValues(producto);
            }
            else
            {
                _bd.Productos.Update(producto);
            }
            return Guardar();
        }

        public bool BorrarProducto(Producto producto)
        {
            _bd.Productos.Remove(producto);
            return Guardar();
        }

        public bool CrearProducto(Producto producto)
        {
            //producto.categoria = _pcService.AsignarCategoria(producto.categoriaId);
            _bd.Productos.Add(producto);
            return Guardar();
        }

        public bool ExisteProducto(long id)
        {
            return _bd.Productos.Any(p => p.Id == id);
        }

        public bool ExisteProducto(string nombre)
        {
            if(nombre == null) { return false; }
            bool valor = _bd.Productos.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Producto GetProducto(long producotId)
        {
            return _bd.Productos.FirstOrDefault(p => p.Id == producotId);
        }

        public ICollection<Producto> GetProductos()
        {
            return _bd.Productos.OrderBy(p => p.Id).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
