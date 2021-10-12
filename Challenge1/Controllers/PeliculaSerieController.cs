using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Controllers
{
    using AutoMapper;
    using global::Challenge1.Data;
    using global::Challenge1.Entities;
    using global::Challenge1.Interfaces;
    using global::Challenge1.ViewModels.PeliculaSerie.CrearPeliculaSerie;
    using global::Challenge1.ViewModels.PeliculaSerie.DetallePeliculaSerie;
    using global::Challenge1.ViewModels.PeliculaSerie.ListarPeliculaSerie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace Challenge1.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]
        public class PeliculaSerieController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            private readonly IPeliculaSerieRepository _repository;
            private readonly IMapper _mapper;
            public PeliculaSerieController(ApplicationDbContext context, IPeliculaSerieRepository repository, IMapper mapper)
            {
                _context = context;
                _repository = repository;
                _mapper = mapper;
            }



            //async

            //listar personajes
            //vamos a declarar que tipo de operación vamos a usar
            [HttpGet("list")]
            public async Task<ActionResult<ListarPeliculaSerieResponseViewModel[]>> GetAllPeliSeriesAsync()
            {

                // ponemos toda la opeacion dentro de un bloque try-catch para ver si algo malo sucede
                try
                {
                    var results = await _repository.GetAllPeliSeriesAsync();
                    //convertir el resultado en un modelo y devolverlo
                    return _mapper.Map<ListarPeliculaSerieResponseViewModel[]>(results);
                }
                catch (Exception)
                {

                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
                }

            }

            //busqueda por id
            // este va a ser un valor que le pasemos al metodo mediante el routeo agregandose al routeo normal
            [HttpGet("detail/{id}")]


            public async Task<ActionResult<DetallePeliculaSerieResponseViewModel>> DetallePeliculaSerie(int id)
            {
                try
                {
                    var result = await _repository.GetPeliSerieAsyncId(id);
                    if (result == null) return NotFound();

                    return _mapper.Map<DetallePeliculaSerieResponseViewModel>(result);
                }
                catch (Exception)
                {

                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
                }

            }

            //busqueda
            [HttpGet("search")]
            public async Task<ActionResult<DetallePeliculaSerieResponseViewModel[]>> SearchPeliculaSerie(string name, int? generoId, string orden)
            {

                if (name!=null & generoId==null) { 
                    try
                    {
                        var resultados = await _repository.GetAllPeliSeriesByName(name, orden);
                        if (!resultados.Any()) return NotFound();

                        return _mapper.Map<DetallePeliculaSerieResponseViewModel[]>(resultados);
                    }
                    catch (Exception)
                    {

                        return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");

                    }
                }

                if (name == null & generoId != null & generoId>0)
                {
                    try
                    {
                        var resultados = await _repository.GetAllPeliSeriesById(generoId, orden);
                        if (!resultados.Any()) return NotFound();

                        return _mapper.Map<DetallePeliculaSerieResponseViewModel[]>(resultados);
                    }
                    catch (Exception)
                    {

                        return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");

                    }
                }







                return this.StatusCode(StatusCodes.Status400BadRequest, "Por favor utilice algún parametro de busqueda");



            }


            //crear objeto
            [HttpPost]
            public async Task<ActionResult<CrearPeliculaSerieResponseViewModel>> CrearPeliculaSerie(CrearPeliculaSerieRequestViewModel model)
            {
                try
                {
                    //campo unico validacion

                    var existePeliserie = await _repository.GetPeliSerieAsyncName(model.Titulo);
                    if (existePeliserie != null)
                    {
                        return BadRequest("Titulo en uso");
                    }


                    var peliserie = _mapper.Map<PeliculaSerie>(model);
                    _repository.Add(peliserie);



                    if (await _repository.SaveChangesAsync())
                    {
                        return Created("", _mapper.Map<CrearPeliculaSerieResponseViewModel>(peliserie));
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
            public async Task<ActionResult<CrearPeliculaSerieResponseViewModel>> ModificarPeliculaSerie(int id, CrearPeliculaSerieRequestViewModel model)
            {
                try
                {
                    var peliserieViejo = await _repository.GetPeliSerieAsyncId(id);
                    if (peliserieViejo == null)
                    {
                        return NotFound($"No se pudo encontrar una pelicula o serie con el nombre {id}");
                    }

                    _mapper.Map(model, peliserieViejo);

                    if (await _repository.SaveChangesAsync())
                    {
                        return _mapper.Map<CrearPeliculaSerieResponseViewModel>(peliserieViejo);
                    }
                }
                catch (Exception)
                {

                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
                }
                return BadRequest();
            }

            [HttpDelete("delete/{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var peliserieViejo = await _repository.GetPeliSerieAsyncId(id);
                    if (peliserieViejo == null)
                    {
                        return NotFound($"No se pudo encontrar una pelicula o serie con el id:  {id}");
                    }

                    _repository.Delete(peliserieViejo);

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

}
