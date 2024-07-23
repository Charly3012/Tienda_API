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


            //Usuario



        }
    }
}
