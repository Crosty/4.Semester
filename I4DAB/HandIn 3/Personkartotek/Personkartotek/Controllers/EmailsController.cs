using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Models;

namespace Personkartotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public EmailsController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the Emails created
        /// </summary>
        /// <returns>A List of Emails has been shown</returns>
        // GET: api/Emails
        [HttpGet]
        public IEnumerable<Email> GetEmail()
        {
            return _context.EmailMigration;
        }

        /// <summary>
        /// Gets a specified Email
        /// </summary>
        /// <returns>A specified Email has been shown</returns>
        // GET: api/Emails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _context.EmailMigration.FindAsync(id);

            if (email == null)
            {
                return NotFound();
            }

            return Ok(email);
        }

        /// <summary>
        /// Updates a specified Email (Creates if it doesn't exist)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "emailId": 0,
        ///         "personId": 0,
        ///         "emailAddress": "string",
        ///         "person": ,
        ///     }
        /// 
        /// </remarks>
        /// <returns>A specified Email has been updated or a Email has been created</returns>
        // PUT: api/Emails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmail([FromRoute] int id, [FromBody] Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != email.EmailId)
            {
                return BadRequest();
            }

            _context.Entry(email).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates an Email
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "emailId": 0,
        ///         "personId": 0,
        ///         "emailAddress": "string",
        ///         "person": ,
        ///     }
        /// 
        /// </remarks>
        /// <returns>A new Email has been created</returns>
        // POST: api/Emails
        [HttpPost]
        public async Task<IActionResult> PostEmail([FromBody] Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmailMigration.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmail", new { id = email.EmailId }, email);
        }

        /// <summary>
        /// Deletes a specified Email
        /// </summary>
        /// <returns>A specified Email has been deleted</returns>
        // DELETE: api/Emails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _context.EmailMigration.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            _context.EmailMigration.Remove(email);
            await _context.SaveChangesAsync();

            return Ok(email);
        }

        private bool EmailExists(int id)
        {
            return _context.EmailMigration.Any(e => e.EmailId == id);
        }
    }
}