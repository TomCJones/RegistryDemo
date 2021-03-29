// AuthorizeController.cs  copyright 2021 tomjones

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistryDemo.Models;
using static RegistryDemo.Models.JsonHelpers;

namespace RegistryDemo.Controllers
{

    [ApiController]
    [Route("[controller]")]   //  ie "Authorize"
    public class AuthorizeController : ControllerBase
    {
        private SqliteDBContext _dbContext;

        private readonly ILogger<AuthorizeController> _logger;

        //  sqlite migrations are not as good as sql server
        //  https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-5.0&tabs=visual-studio

        public AuthorizeController(ILogger<AuthorizeController> logger, RegistryDemo.Models.SqliteDBContext context)
        {
            _logger = logger;
            _dbContext = context;
            Console.WriteLine();
            Console.WriteLine("Authorize Request Code Initialized");
        }
        // Controller action return types in ASP.NET Core web API  https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0
        [HttpGet]
        public async Task<string> Get(string? balance)
        {
            HttpRequest request1 = HttpContext.Request;
            String query = request1.QueryString.ToString();
            string resp = "I'm a response message to the query " + query;
            return resp ;
        }
    }
}
