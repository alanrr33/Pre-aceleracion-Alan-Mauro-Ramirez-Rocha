using Challenge1.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
//creamos una db en memoria para usarla en las unit tests
namespace Characters.UnitTests
{
    public class InMemoryDbContext
    {
        public ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryPersonajeDatabase")
                            .Options;
            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }

    }

}
