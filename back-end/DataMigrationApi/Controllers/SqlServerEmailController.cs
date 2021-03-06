﻿using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlServerEmailController : ControllerBase
    {
        private readonly ISqlServerEmailService _emailService;

        public SqlServerEmailController(ISqlServerEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Email>> GetAllUserEmails(string id, [FromQuery]EmailParameters parameters)
        {
            var emails = _emailService.GetAllUserEmails(id, parameters);
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
            if (!ModelState.IsValid)
            {
                return BadRequest("Email is not valid!");
            }
            email.UserID = userId;
            var inserted = _emailService.Insert(email);
            return inserted;
        }

        [HttpPut("{id}")]
        public ActionResult<Email> Put([FromBody] Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Email is not valid!");
            }
            var updated = _emailService.Update(email);
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
