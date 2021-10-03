using AutoMapper;
using Challenge1.Data;
using Challenge1.Entities;
using Challenge1.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using Challenge1.ViewModels.Personaje.ListarPersonajes;
using Challenge1.ViewModels.Personaje.CrearPersonaje;
using Challenge1.ViewModels.Personaje.DetallePersonaje;

namespace Challenge1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonajeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonajesRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        public PersonajeController(ApplicationDbContext context, IPersonajesRepository repository, IMapper mapper,LinkGenerator linkGenerator)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        //async

        //listar personajes
        //vamos a declarar que tipo de operación vamos a usar
        [HttpGet("listar")]
        public async Task<ActionResult<ListarPersonajesResponseViewModel[]>> ListarPersonajes()
        {

            // ponemos toda la operacion dentro de un bloque try-catch para ver si algo malo sucede
            try
            {
                var results = await _repository.GetAllPersonajesAsync();
                //convertir el resultado en un modelo y devolverlo
                return _mapper.Map<ListarPersonajesResponseViewModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }

        }

        //detalle por id
        // este va a ser un valor que le pasemos al metodo mediante el routeo agregandose al routeo normal
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<DetallePersonajeResponseViewModel>> Get(int id)
        {
            try
            {
                var result = await _repository.GetPersonajeIdAsync(id);
                if (result == null) return NotFound();

                return _mapper.Map<DetallePersonajeResponseViewModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }

        }

        //busqueda
        [HttpGet("search")]
        public async Task<ActionResult<CrearPersonajeResponseViewModel[]>> SearchPersonaje(string name,int? edad, float? peso, int? peliSerieId)
        {

            if (name!=null | edad!=null | peso!=null | peliSerieId!=null) { 
                try
                {
                    var resultados = await _repository.GetAllPersonajesByName(name,edad,peso,peliSerieId);
                    if (!resultados.Any()) return NotFound();

                    return _mapper.Map<CrearPersonajeResponseViewModel[]>(resultados);
                }
                catch (Exception)
                {

                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");

                }

            }
            return this.StatusCode(StatusCodes.Status400BadRequest, "Por favor utilice algún parametro de busqueda");
        }

        //crear objeto
        [HttpPost("create")]
        public async Task<ActionResult<CrearPersonajeResponseViewModel>> CrearPersonaje(CrearPersonajeRequestViewModel model)
        {
            try
            {
                //campo unico validacion

                var existePj = await _repository.GetPersonajeNameAsync(model.Nombre);
                if (existePj != null)
                {
                    return BadRequest("Nombre en uso");
                }

                var personaje = _mapper.Map<Personaje>(model);
                _repository.Add(personaje);

                if(await _repository.SaveChangesAsync())
                {
                    return Created("",_mapper.Map<CrearPersonajeResponseViewModel>(personaje));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }
            return BadRequest();
        }

        //modificar objeto

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CrearPersonajeRequestViewModel>> ModificarPersonaje(int id, CrearPersonajeRequestViewModel model)
        {
            try
            {
                var pjViejo = await _repository.GetPersonajeIdAsync(id);
                if (pjViejo== null)
                {
                    return NotFound($"No se pudo encontrar un personaje con el id {id}");
                }

                _mapper.Map(model, pjViejo);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CrearPersonajeRequestViewModel>(pjViejo);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }
            return BadRequest();
        }

        //borrar objeto
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                var pjViejo = await _repository.GetPersonajeIdAsync(id);
                if (pjViejo == null)
                {
                    return NotFound($"No se pudo encontrar un personaje con el id {id}");
                }

                _repository.Delete(pjViejo);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }

            return BadRequest();
        }

    }


}
