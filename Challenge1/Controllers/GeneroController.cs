using AutoMapper;
using Challenge1.Data;
using Challenge1.Entities;
using Challenge1.Interfaces;
using Challenge1.ViewModels.Genero.Get;
using Challenge1.ViewModels.Genero.Post;
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
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGeneroRepository _repository;
        private readonly IMapper _mapper;
        public GeneroController(ApplicationDbContext context, IGeneroRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }


        //async

        //listar generos
        //vamos a declarar que tipo de operación vamos a usar
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<GetAllGenerosResponseViewModel[]>> GetAllGenerosAsync()
        {

            // ponemos toda la opeacion dentro de un bloque try-catch para ver si algo malo sucede
            try
            {
                var results = await _repository.GetAllGenerosAsync();
                //convertir el resultado en un modelo y devolverlo
                return _mapper.Map<GetAllGenerosResponseViewModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }

        }

        //busqueda
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<GetAllGenerosResponseViewModel[]>> SearchByName(string name, bool ordenAsc = true)
        {
            if (name != null) 
            { 
                
                    try
                {
                    var resultados = await _repository.GetAllGenerosNameAsync(name, ordenAsc);
                    if (!resultados.Any()) return NotFound();
                
                
                    return _mapper.Map<GetAllGenerosResponseViewModel[]>(resultados);
                }
                catch (Exception)
                {

                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");

                }
            }
            return this.StatusCode(StatusCodes.Status400BadRequest, "Por favor utilice algún parametro de busqueda");

        }

        //crear
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CrearGeneroRequestViewModel>> CrearGenero(CrearGeneroRequestViewModel model)
        {
            try
            {
                //campo unico validacion

                var existePeliserie = await _repository.GetGeneroNameAsync(model.Nombre);
                if (existePeliserie != null)
                {
                    return BadRequest("Titulo en uso");
                }

                var genero = _mapper.Map<Genero>(model);
                _repository.Add(genero);

                if (await _repository.SaveChangesAsync())
                {
                    return Created("", _mapper.Map<CrearGeneroResponseViewModel>(genero));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }
            return BadRequest();
        }

        //modificar
        [HttpPut("update/{id}")]
        public async Task<ActionResult<CrearGeneroRequestViewModel>> Put(int id, CrearGeneroRequestViewModel model)
        {
            try
            {
                var generoViejo = await _repository.GetGeneroIdAsync(id);
                if (generoViejo == null)
                {
                    return NotFound($"No se pudo encontrar un genero con el id {id}");
                }

                //aplicamos los cambios usando el automapper

                _mapper.Map(model, generoViejo);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CrearGeneroRequestViewModel>(generoViejo);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Fallo db");
            }
            return BadRequest();
        }

        //borrar
        [HttpDelete("delete/{id}")]
       
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var genero = await _repository.GetGeneroIdAsync(id);
                if (genero == null)
                {
                    return NotFound($"No se pudo encontrar un genero con el id:  {id}");
                }

                _repository.Delete(genero);

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

