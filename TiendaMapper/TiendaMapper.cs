using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using AutoMapper;

namespace ApiTienda.TiendaMapper
{
    public class TiendaMapper : Profile
    {

        public TiendaMapper() {

            //Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDTO>().ReverseMap();


            //Producto
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Producto, CrearProductoDTO>().ReverseMap();


            //Usuario


            //Ventas
            CreateMap<Venta, CrearVentaDTO>().ReverseMap();
            CreateMap<Venta, VentaDTO>().ReverseMap();


            //Detalles venta
            CreateMap<DetalleVenta, CrearDetalleVentaDTO>().ReverseMap();
            CreateMap<DetalleVenta, DetalleVentaDTO>().ReverseMap();



        }
    }
}
