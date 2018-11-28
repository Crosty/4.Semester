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
    public class PeopleController : ControllerBase
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public PeopleController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the People created
        /// </summary>
        /// <returns>A List of People has been shown</returns>
        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPerson()
        {
            return _context.PersonMigration;
        }

        /// <summary>
        /// Gets a specified Person
        /// </summary>
        /// <returns>A specified Person has been shown</returns>
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.PersonMigration.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Updates a specified Person (Creates if it doesn't exist)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "personId": 0,
        ///         "personType": "string",
        ///         "firstName": "string",
        ///         "middleName": "string",
        ///         "lastName": "string",
        ///         "address": ,
        ///         "email": ,
        ///     }
        ///     
        /// </remarks>
        /// <returns>A specified Person has been updated or a Person has been created</returns>
        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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
        /// Creates a person
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "personId": 0,
        ///         "personType": "string",
        ///         "firstName": "string",
        ///         "middleName": "string",
        ///         "lastName": "string",
        ///         "address": ,
        ///         "email": ,
        ///     }
        ///     
        /// </remarks>
        /// <returns>A new Person has been created</returns>
        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PersonMigration.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        /// <summary>
        /// Deletes a specified Person
        /// </summary>
        /// <returns>A specified Person has been deleted</returns>
        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.PersonMigration.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.PersonMigration.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        private bool PersonExists(int id)
        {
            return _context.PersonMigration.Any(e => e.PersonId == id);
        }
    }
}