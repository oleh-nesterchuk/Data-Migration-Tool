using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataMigrationApi.Controllers
{
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

            var inserted = _sqlUserService.Insert(user);
            foreach (var e in emails)
            {
                e.UserID = inserted.ID;
                _sqlEmailService.Insert(e);
            }

            return inserted;
        }
    }
}