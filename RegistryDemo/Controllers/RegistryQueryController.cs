// RegistryQueyrController.cs  copyright 2012 tomjones

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistryDemo.Models;
using static RegistryDemo.Models.JsonHelpers;

namespace RegistryDemo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RegistryQueryController : ControllerBase
    {
        private SqliteDBContext _dbContext;

        private readonly ILogger<RegistryQueryController> _logger;

        //  sqlite migrations are not as good as sql server
        //  https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-5.0&tabs=visual-studio

        public RegistryQueryController(ILogger<RegistryQueryController> logger, RegistryDemo.Models.SqliteDBContext context)
        {
            _logger = logger;
            _dbContext = context;
            Console.WriteLine();
            context.Database.EnsureCreated();
            Console.WriteLine("DB has been Created - pre migrate");
     //       context.Database.Migrate();
     //       Console.WriteLine("Post migrate");
            if (context.RegisteredObjects.Any())
            {
                Console.WriteLine("context created - we have Registered Objects");
                return;
            }
            string dbPath = context.Database.GetConnectionString();
            string tdPath = "TestData.json";
            string testData = System.IO.File.ReadAllText(tdPath, Encoding.UTF8);
            List<Maas> Ldo;
            Ldo = GetJsonGenericType<List<Maas>>(testData);
            foreach (Maas maas in Ldo)
            {
                RegisteredObject ro = new RegisteredObject
                {
                    Name = maas.name,
                    Version = maas.version,
                    Platform = maas.platform,
                    Min_platform = maas.min_platform,
                    Source = maas.source,
                    Jurisdiction = maas.jurisdiction,
                    User_authn = maas.user_authn,
                    DateRegistered = maas.date,
                    Url = maas.url,
                    Trust_registry = maas.trust_registry
                };
                context.Add(ro);
            }
            context.SaveChanges();
            Console.WriteLine("Completed Creation of base Registered Objtecs");
        }
        // Controller action return types in ASP.NET Core web API  https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0
        [HttpGet]
        public async  IAsyncEnumerable<RegisteredObject> Get()
        {
            var rng = new Random();

            IEnumerable<RegisteredObject> ero = await _dbContext.GetObjectsAsync();
            foreach (RegisteredObject ro in ero)
            {
               yield return ro;
            }
        }
    }
}
