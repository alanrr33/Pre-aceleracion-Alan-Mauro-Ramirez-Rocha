using Challenge1.Controllers;
using System;
using Xunit;

namespace ChallengeTest.Tests
{
    public class PersonajesControllerTests
    {
        [Fact]
        public void Test1()
        {
            //Arrange

            //vamos a tener que crear
            //una instancia de la interfaz de la db para poder realizar las pruebas
            //instalando el paquete FakeItEasy
            var controller = new PersonajeController();

            //Act

            //Assert

        }
    }
}
