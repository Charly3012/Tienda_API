using ApiTienda.Exceptions;
using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using ApiTienda.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiTienda.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _prRepo;
        private readonly IMapper _mapper;
        private readonly IProductoCategoriaService _pcService;

        public ProductoController(IProductoRepository prRepo, IMapper mapper, IProductoCategoriaService pcService)
        {
            _prRepo = prRepo;
            _mapper = mapper;
            _pcService = pcService;

        }


        [HttpGet(Name = "GetProductos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductos()
        {
            var listaProductos = _prRepo.GetProductos();

            var listaProdcutosDTO = new List<ProductoDTO>();

            foreach (var producto in listaProductos)
            {
                producto.categoria = _pcService.AsignarCategoria(producto.categoriaId);
                listaProdcutosDTO.Add(_mapper.Map<ProductoDTO>(producto));
            }
            return Ok(listaProdcutosDTO);
        }

        [HttpGet("{productoId:long}", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProducto(long productoId)
        {
            var itemProducto = _prRepo.GetProducto(productoId);

            //Checa que si exista
            if (itemProducto == null)
            {
                return NotFound();
            }
            var itemProductoDTO = _mapper.Map<ProductoDTO>(itemProducto);
            itemProductoDTO.categoria = _pcService.AsignarCategoria(itemProducto.categoriaId);

            return Ok(itemProductoDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostProducto(CrearProductoDTO crearProductoDTO)
        {
            //Checar que el modelo sea valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (_prRepo.ExisteProducto(crearProductoDTO.Nombre))
            {
                ModelState.AddModelError("", "El producto ya existe");
                return StatusCode(409, ModelState);
            }


            var producto = _mapper.Map<Producto>(crearProductoDTO);

            try
            {
                producto.categoria = _pcService.AsignarCategoria(producto.categoriaId);
                if (!_prRepo.CrearProducto(producto))
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro {producto.Nombre}");
                    return StatusCode(404, ModelState);
                }
            }
            catch (CategoriaNotFoundException e)
            {
                ModelState.AddModelError("Error", e.Message);
                return NotFound(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error interno del servidor: {e.Message}");
            }

            

            return CreatedAtRoute("GetProducto", new { productoId = producto.Id }, producto);
        }


        [HttpPatch("{productoId:long}", Name = "ActualizarProductoPatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarProductoPatch(long productoId, [FromBody] ActualizarProductoDTO actualizarProductoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (actualizarProductoDTO == null || actualizarProductoDTO.Id != productoId)
            {
                ModelState.AddModelError("Error", "El ID del path debe coincidir con el ID del body");
                return BadRequest(ModelState);
            }


            //Validar si existe
            var productoExistente = _prRepo.GetProducto(productoId);

            if (productoExistente == null)
            {
                ModelState.AddModelError("Error", $"No se encontró el producto con Id: {productoId}");
                return NotFound(ModelState);
            }


            //Verificar que no se repita
            if (_prRepo.ExisteProducto(actualizarProductoDTO.Nombre) &&
                actualizarProductoDTO.Nombre.ToLower().Trim() != productoExistente.Nombre.ToLower().Trim())
            {
                ModelState.AddModelError("", $"Ya existe un producto con el nombre: '{actualizarProductoDTO.Nombre}'");
                return StatusCode(409, ModelState);
            }
            try
            {
                productoExistente = _pcService.ActualizarProducto(productoExistente, actualizarProductoDTO);

                //Aquí se actualiza
                if (!_prRepo.ActualizarProducto(productoExistente))
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro {productoExistente.Nombre})");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (CategoriaNotFoundException e)
            {
                ModelState.AddModelError("Error", e.Message);
                return NotFound(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error interno del servidor: {e.Message}");
            }

        }


        [HttpDelete("{productoId:long}", Name = "BorrarProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarProducto(long productoId)
        {

            if (!_prRepo.ExisteProducto(productoId))
            {
                ModelState.AddModelError("Error", $"No se encontro el producto con el id: {productoId}");
                return NotFound(ModelState);
            }

            var categoria = _prRepo.GetProducto(productoId);
            if (!_prRepo.BorrarProducto(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
