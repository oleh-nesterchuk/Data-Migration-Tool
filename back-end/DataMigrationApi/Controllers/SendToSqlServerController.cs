using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DataMigrationApi.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SendToSqlServerController : ControllerBase
    {
        private readonly ISqlServerUserService _sqlUserService;
        private readonly ISqlServerEmailService _sqlEmailService;
        private readonly IMongoDbUserService _mongoService;

        public SendToSqlServerController(ISqlServerUserService sqlService,
            ISqlServerEmailService sqlEmailService,
            IMongoDbUserService mongoService)
        {
            _sqlUserService = sqlService;
            _sqlEmailService = sqlEmailService;
            _mongoService = mongoService;
        }

        [HttpGet("{id}")]
        public ActionResult<User> Transfer(string id)
        {
            if (_sqlUserService.Get(id) != null)
            {
                return BadRequest("Object is already absent at SQL Server!");
            }
            var user = _mongoService.Get(id);
            var emails = user.Emails;
            user.Emails = null;
            user.Identity = 0;

            var inserted = _sqlUserService.Insert(user);
            foreach (var e in emails)
            {
                e.ID = 0;
                e.UserID = inserted.ID;
                _sqlEmailService.Insert(e);
            }

            return inserted;
        }
    }
}