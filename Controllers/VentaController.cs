using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTienda.Controllers
{
    [Route("api/Venta")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        //Inyección
        private readonly IVentaRepository _veRepo;
        private readonly IMapper _mapper;

        public VentaController(IVentaRepository veRepo, IMapper mapper)
        {
            _veRepo = veRepo;
            _mapper = mapper;
        }



        //Métodos HTTP

        [HttpGet(Name = "GetVentas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVentas()
        {
            var listaVentas = _veRepo.GetVentas();
            var listaVentasDTO = new List<VentaDTO>();
            foreach (var item in listaVentas)
            {
                listaVentasDTO.Add(_mapper.Map<VentaDTO>(item));
            }
            return Ok(listaVentasDTO);
        }

        [HttpGet("{ventaId:long}", Name = "GetVenta")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVenta(long ventaId)
        {
            var itemVenta = _veRepo.GetVenta(ventaId);

            //Checa que si exista
            if (itemVenta == null)
            {
                return NotFound();
            }
            var itemVentaDTO = _mapper.Map<VentaDTO>(itemVenta);

            return Ok(itemVentaDTO);

        }
    }
}
