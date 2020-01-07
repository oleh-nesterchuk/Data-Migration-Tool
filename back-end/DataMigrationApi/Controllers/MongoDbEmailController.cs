using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDbEmailController : ControllerBase
    {
        private readonly IMongoDbEmailService _emailService;

        public MongoDbEmailController(IMongoDbEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Email>> GetAllUserEmails(string id)
        {
            var emails = _emailService.GetAllUserEmails(id);
            if (emails == null)
            {
                return NotFound();
            }

            return emails.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Email> Get(int id)
        {
            var email = _emailService.Get(id);
            if (email == null)
            {
                return NotFound();
            }

            return email;
        }

        [HttpPost]
        public ActionResult<Email> Post([FromBody] Email email, string userId)
        {
            var inserted = _emailService.Insert(email, userId);
            if (inserted == null)
            {
                return NotFound();
            }
            return inserted;
        }

        [HttpPut("{id}")]
        public ActionResult<Email> Put([FromBody] Email email, string userId)
        {
            var updated = _emailService.Update(email, userId);
            if (updated == null)
            {
                return NotFound();
            }
            return updated;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _emailService.Delete(id);
            return NoContent();
        }
    }
}
