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
    public class ZipsController : ControllerBase
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public ZipsController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all the Zips created
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "zipId": 0,
        ///         "city": "string",
        ///         "country": "string",
        ///         "zipCode": "string"
        ///     }
        /// 
        /// </remarks>
        /// <returns>A List of Zips has been shown</returns>
        // GET: api/Zips
        [HttpGet]
        public IEnumerable<Zip> GetZip()
        {
            return _context.ZipMigration;
        }

        /// <summary>
        /// Gets a specified Zip
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "zipId": 0
        ///     }
        /// 
        /// </remarks>
        /// <returns>A specified Zip has been shown</returns>
        // GET: api/Zips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zip = await _context.ZipMigration.FindAsync(id);

            if (zip == null)
            {
                return NotFound();
            }

            return Ok(zip);
        }

        /// <summary>
        /// Updates a specified Zip (Creates if it doesn't exist)
        /// </summary>
        /// <returns>A specified Zip has been updated or a Zip has been created</returns>
        // PUT: api/Zips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZip([FromRoute] int id, [FromBody] Zip zip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zip.ZipId)
            {
                return BadRequest();
            }

            _context.Entry(zip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZipExists(id))
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
        /// Creates a Zip
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Zip
        ///     {
        ///         "zipId": 0,
        ///         "city": "string",
        ///         "country": "string",
        ///         "zipCode": "string",
        ///         "address": []
        ///     }
        /// 
        /// </remarks>
        /// <param name="zip"></param>
        /// <returns>A new Zip has been created</returns>
        // POST: api/Zips
        [HttpPost]
        public async Task<IActionResult> PostZip([FromBody] Zip zip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ZipMigration.Add(zip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZip", new { id = zip.ZipId }, zip);
        }

        /// <summary>
        /// Deletes a specified Zip
        /// </summary>
        /// <returns>A specified Zip has been deleted</returns>
        // DELETE: api/Zips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zip = await _context.ZipMigration.FindAsync(id);
            if (zip == null)
            {
                return NotFound();
            }

            _context.ZipMigration.Remove(zip);
            await _context.SaveChangesAsync();

            return Ok(zip);
        }

        private bool ZipExists(int id)
        {
            return _context.ZipMigration.Any(e => e.ZipId == id);
        }
    }
}