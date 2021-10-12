using AutoMapper;
using Challenge1.Controllers;
using Challenge1.Data;
using Challenge1.Entities;
using Challenge1.Interfaces;
using Challenge1.Repositories;
using Challenge1.ViewModels.Personaje.CrearPersonaje;
using Challenge1.ViewModels.Personaje.ListarPersonajes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Characters.UnitTests
{
    public class PersonajesControllerTests
    {
        private readonly ApplicationDbContext _dbContext;
        public PersonajesControllerTests()
        {
            _dbContext = new InMemoryDbContext().GetDbContext();

        }


        [Fact]
        public async Task Get_ConPersonajeInexistente_ReturnsNotFound()
        {

            //arrange
            //preparamos todo para que este listo para ser ejecutado
            Random rnd = new Random();

            //agregamos algo de datos a la bd

            var personajes = new List<Personaje>
                {
                    new Personaje { Id = 100001 },
                    new Personaje { Id = 100002 },
                    new Personaje { Id = 100003 },
                    new Personaje { Id = 100004 }
                };
            _dbContext.Personajes.AddRange(personajes);
            _dbContext.SaveChanges();


            //creamos los stubs para el controller
            var repositoryStub = new Mock<IPersonajesRepository>();
            var mapperStub = new Mock<IMapper>();

            //configuramos para que cuando el controlador llame al metodo getpersonajeidasync con un parametro
            //int cualquier que va a ser provisto por Moq le diga que me tiene que devolver un objeto Personaje null
            repositoryStub.Setup(repo => repo.GetPersonajeIdAsync(It.IsAny<int>())).ReturnsAsync((Personaje)null);

            //le pasamos como parametro la propiedad obj de los stubs
            var controller = new PersonajeController(_dbContext, repositoryStub.Object, mapperStub.Object);

            //act
            //ejecutamos las acciones

            var result = await controller.Get(rnd.Next(1, 100000));


            //assert
            //verficamos la ejecución


            Assert.IsType<NotFoundResult>(result.Result);


        }

        [Fact]
        public async Task GetPersonajesAsync_ConPersonajesExistentes_ReturnsTodosLosPjs()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonajeProfile()); //automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            //arrange
            

            var arrayPjs = new[]
            {
                new Personaje { Id = 100001,Nombre="Pj1",Edad=20,Peso=30,Historia="xdddd" }
            };

            //creamos los stubs para el controller
            var repositoryStub = new Mock<IPersonajesRepository>();
            var mapperStub = new Mock<IMapper>();

            //configuramos para que cuando el controlador llame al metodo getpersonajeidasync con un parametro
            //int cualquier que va a ser provisto por Moq le diga que me tiene que devolver un objeto  array Personaje
         
            repositoryStub.Setup(repo => repo.GetAllPersonajesAsync()).ReturnsAsync(arrayPjs);

            //le pasamos como parametro la propiedad obj de los stubs al controller
            var controller = new PersonajeController(_dbContext, repositoryStub.Object,mapper);


            //act
            
            var pjsTraidos = await controller.ListarPersonajes();


            //assert

            pjsTraidos.Value.Should().BeEquivalentTo(
                arrayPjs,
                options => options.ComparingByMembers<Personaje>().ExcludingNestedObjects().ExcludingMissingMembers()
                );

        }

        [Fact]
        public async Task CrearPersonajeAsync_ConPersonajeParaCrear_ReturnsPjCreado()
        {
            //arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonajeProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var pjParaCrear = new CrearPersonajeRequestViewModel
            {
                Imagen = "imagen Alan",
                Nombre = "Alan",
                Edad = 333,
                Peso = 300,
                Historia = "123 que bonito"

            };


            //creamos los stubs para el controller
            var repositoryStub = new Mock<IPersonajesRepository>();

            // configurando el repositorio
            repositoryStub.Setup(repo => repo.GetPersonajeNameAsync(It.IsAny<string>())).ReturnsAsync((Personaje)null);
            repositoryStub.Setup(repo => repo.Add(It.IsAny<Personaje>()));
            repositoryStub.Setup(repo => repo.SaveChangesAsync().Result).Returns(true);



            //le pasamos como parametro la propiedad obj de los stubs al controller
            var controller = new PersonajeController(_dbContext, repositoryStub.Object, mapper);

            //act

            var result = await controller.CrearPersonaje(pjParaCrear);

            //assert

            var pjCreado =(result.Result as CreatedResult).Value as CrearPersonajeResponseViewModel;

            pjParaCrear.Should().BeEquivalentTo(
                pjCreado,
                options => options.ComparingByMembers<CrearPersonajeResponseViewModel>()
                .ExcludingMissingMembers()
                );
        }

        [Fact]
        public async Task CrearPersonajeAsync_ConPersonajeParaCrearYNombreYaEnUso_ReturnsBadObjectRequest()
        {
            //arrange
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PersonajeProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();


            var pjParaCrear = new CrearPersonajeRequestViewModel
            {
                Imagen = "imagen Alan",
                Nombre = "Alan",
                Edad = 333,
                Peso = 300,
                Historia = "123 que bonito"

            };


            //creamos los stubs para el controller
            var repositoryStub = new Mock<IPersonajesRepository>();

            // configurando el repositorio
            //para que usando "cualquier nombre" me devuelva un "nuevo Personaje"
            repositoryStub.Setup(repo => repo.GetPersonajeNameAsync(It.IsAny<string>())).ReturnsAsync((Personaje)new());


            //le pasamos como parametro la propiedad obj de los stubs al controller
            var controller = new PersonajeController(_dbContext, repositoryStub.Object, mapper);

            //act

            var result = await controller.CrearPersonaje(pjParaCrear);

            //assert

            var existePj = (result.Result as BadRequestObjectResult);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }


    }
}
