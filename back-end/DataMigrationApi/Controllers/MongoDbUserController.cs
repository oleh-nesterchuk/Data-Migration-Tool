using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDbUserController : ControllerBase
    {
        private readonly IMongoDbUserService _userService;

        public MongoDbUserController(IMongoDbUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetSize")]
        public ActionResult<int> GetSize() =>
            _userService.GetSize();

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery]UserParameters parameters) =>
              _userService.Get(parameters).ToList();

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User is not valid!");
            }
            var inserted = _userService.Insert(user);
            return inserted;
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User is not valid!");
            }
            var updated = _userService.Update(user);
            if (updated == null)
            {
                return NotFound();
            }

            return updated;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
