using ApiTienda.Exceptions;
using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using ApiTienda.Services.IServices;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace ApiTienda.Services
{
    public class VentaService : IVentaService
    {
        //IVA
        public const double _valorIva = 0.16;

        //Inyección
        private readonly IMapper _mapper;
        private readonly IProductoRepository _prRepo;

        public VentaService(IMapper mapper, IProductoRepository prRepo)
        {
            _mapper = mapper;
            _prRepo = prRepo;
        }


        public Venta CrearVenta(CrearVentaDTO crearVentaDTO)
        {
            //Validar la lista de productos
            if (crearVentaDTO.DetallesVenta == null)
            {
                throw new ArgumentException("La lista de productos no debe estar vacía");
            }

            var ventaN = new Venta()
            {
                Fecha = DateTime.Now,
                Productos = new List<DetalleVenta>()
            };

            double subtotal = 0;
            foreach (var item in crearVentaDTO.DetallesVenta)
            {
                var producto = _prRepo.GetProducto(item.ProductoId);
                if (producto == null)
                {
                    throw new ProductoNotFoundException($"No se encontró el producto con el id: {item.ProductoId}");
                }
                if (!(producto.Stock >= item.Cantidad))
                {
                    throw new ProductoOutStockException($"No hay suficiente stock para el producto: {producto.Nombre}");
                } 

                var detalleVenta = new DetalleVenta(producto, item.Cantidad, item.ProductoId);
                subtotal = subtotal + detalleVenta.Subtotal;
                ventaN.Productos.Add(detalleVenta);
            }

            ventaN.Subtotal = subtotal;
            ventaN.Iva = subtotal * _valorIva;
            ventaN.Total = subtotal + ventaN.Iva;

            return ventaN;
        }

        public void ActualizarStock(Venta venta)
        {
            foreach(var item in venta.Productos)
            {
                var productoActualizar = _prRepo.GetProducto(item.ProductoId);
                productoActualizar.Stock = productoActualizar.Stock - item.Cantidad;
                _prRepo.ActualizarProducto(productoActualizar);
            }

        }

       
    }
}
