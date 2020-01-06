using DataMigrationApi.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DataMigrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendToMongoDbController : ControllerBase
    {
        private readonly ISqlServerUserService _sqlUserService;
        private readonly ISqlServerEmailService _sqlEmailService;
        private readonly IMongoDbUserService _mongoService;

        public SendToMongoDbController(ISqlServerUserService sqlService,
            ISqlServerEmailService sqlEmailService,
            IMongoDbUserService mongoService)
        {
            _sqlUserService = sqlService;
            _sqlEmailService = sqlEmailService;
            _mongoService = mongoService;
        }

        [HttpGet("{id}")]
        public IActionResult Transfer(string id)
        {
            if (_mongoService.Get(id) != null)
            {
                return BadRequest("Object is already absent at mongodb!");
            }
            var user = _sqlUserService.Get(id);
            user.Emails = _sqlEmailService.GetAllUserEmails(id).ToList();
            _mongoService.Insert(user);
            return Ok();
        }
    }
}