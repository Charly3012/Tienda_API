using ApiTienda.Data;
using ApiTienda.Models;
using ApiTienda.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Repository
{
    public class VentaRepository : IVentaRepository
    {
        //Inyección de dependencias 
        private readonly ApplicationDBContext _bd;
        public VentaRepository(ApplicationDBContext bd)
        {
            _bd = bd;
        }


        public bool ActualizarVenta(Venta venta)
        {
            var ventaExistente = _bd.Ventas.Find(venta.Id);
            if (ventaExistente != null)
            {
                _bd.Entry(ventaExistente).CurrentValues.SetValues(venta);
            }
            else
            {
                _bd.Ventas.Update(venta);
            }
            return Guardar();
        }

        public bool BorrarVenta(Venta venta)
        {
            _bd.Ventas.Remove(venta);
            return Guardar();
        }

        public bool CrearVenta(Venta venta)
        {
            _bd.Ventas.Add(venta);
            return Guardar();
        }

        public bool ExisteVenta(long id)
        {
            return _bd.Ventas.Any(v => v.Id == id);
        }

        public Venta GetVenta(long ventaId)
        {
            return _bd.Ventas.Include(v => v.Productos).FirstOrDefault(v => v.Id == ventaId);
        }

        public ICollection<Venta> GetVentas()
        {
            return _bd.Ventas.Include(v => v.Productos).OrderBy(v => v.Id).ToList();
         }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
