using ApiTienda.Exceptions;
using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using ApiTienda.Services.IServices;
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
        private readonly IVentaService _vService;

        public VentaController(IVentaRepository veRepo, IMapper mapper, IVentaService vService)
        {
            _veRepo = veRepo;
            _mapper = mapper;
            _vService = vService;
        }



        //Métodos HTTP

        [HttpGet(Name = "GetVentas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVentas()
        {
            var listaVentas = _veRepo.GetVentas();
            var listaVentasDTO = new List<VentasVariasDTO>();
            foreach (var item in listaVentas)
            {
                listaVentasDTO.Add(_mapper.Map<VentasVariasDTO>(item));
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostVenta(CrearVentaDTO crearVentaDTO)
        {
            //Checar que el modelo sea valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var venta = _vService.CrearVenta(crearVentaDTO);
                if (!_veRepo.CrearVenta(venta))
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro");
                    return StatusCode(404, ModelState);
                }
                _vService.ActualizarStock(venta);
                return CreatedAtRoute("GetVenta", new { ventaId = venta.Id }, venta);
            }
            catch (ProductoNotFoundException e)
            {
                ModelState.AddModelError("Producto no encontrado", e.Message);
                return NotFound(ModelState);
            }
            catch (ProductoOutStockException e)
            {
                ModelState.AddModelError("Error de stock", e.Message);
                return Conflict(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error interno del servidor: {e.Message}");
            }
        }

        //El patch luego pienso si es buena idea agregarlo 




        [HttpDelete("{ventaId:long}", Name = "BorrarVenta")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarCategoria(long ventaId)
        {

            if (!_veRepo.ExisteVenta(ventaId))
            {
                ModelState.AddModelError("Error", $"No se encontro la venta con el id: {ventaId}");
                return NotFound(ModelState);
            }

            var categoria = _veRepo.GetVenta(ventaId);
            if (!_veRepo.BorrarVenta(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }
}
