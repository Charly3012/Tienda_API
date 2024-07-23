using ApiTienda.Models;
using ApiTienda.Models.DTOs;

namespace ApiTienda.Services.IServices
{
    public interface IVentaService
    {
        Venta CrearVenta(CrearVentaDTO crearVentaDTO);
        void ActualizarStock(Venta venta);
    }
}
