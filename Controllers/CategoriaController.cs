using ApiTienda.Models;
using ApiTienda.Models.DTOs;
using ApiTienda.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTienda.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        //Inyección de dependencias 
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }



        //Métodos HTTP

        [HttpGet(Name = "GetCategorias")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();

            var listaCategoriasDTO = new List<CategoriaDTO>();

            foreach (var item in listaCategorias)
            {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDTO>(item));
            }

            return Ok(listaCategoriasDTO);
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoriaEspecifica")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoriaEspecifica(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            //Checa que si exista
            if (itemCategoria == null)
            {
                return NotFound();
            }
            var itemCategoriaDTO = _mapper.Map<CategoriaDTO>(itemCategoria);

            return Ok(itemCategoriaDTO);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDTO crearCategoriaDTO)
        {
            //Checar que el modelo sea valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validar de que sea null
            if (crearCategoriaDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteCategoria(crearCategoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La categoría ya existe");
                return StatusCode(409, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDTO);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.Nombre}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetCategoriaEspecifica", new { categoriaId = categoria.Id }, categoria);
        }


        [HttpPatch("{categoriaId:long}", Name = "ActualizarCategoriaPatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarCategoriaPatch(long categoriaId, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoriaDTO == null || categoriaDTO.Id != categoriaId)
            {
                ModelState.AddModelError("Error", "El ID del path debe coincidir con el ID del body");
                return BadRequest(ModelState);
            }


            //Validar si existe
            var categoriaExistente = _ctRepo.GetCategoria(categoriaId);

            if (categoriaExistente == null)
            {
                ModelState.AddModelError("Error", $"No se encontró la categoria con Id: {categoriaId}");
                return NotFound(ModelState);
            }

          
            //Verificar que no se repita la categoría
            if (_ctRepo.ExisteCategoria(categoriaDTO.Nombre) && 
                categoriaDTO.Nombre.ToLower().Trim() != categoriaExistente.Nombre.ToLower().Trim())
            {
                ModelState.AddModelError("", $"Ya existe una categoría con el nombre: '{categoriaDTO.Nombre}'");
                return StatusCode(409, ModelState);
            }

            //var categoria = _mapper.Map<Categoria>(categoriaDTO);

            categoriaExistente.ActualizarCategoria(categoriaDTO);


            //Aquí se actualiza
            if (!_ctRepo.ActualizarCategoria(categoriaExistente))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoriaExistente.Nombre})");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarCategoria(int categoriaId)
        {

            if (!_ctRepo.ExisteCategoria(categoriaId))
            {
                ModelState.AddModelError("Error", $"No se encontro la categoría con el id: {categoriaId}");
                return NotFound(ModelState);
            }

            var categoria = _ctRepo.GetCategoria(categoriaId);
            if (!_ctRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
