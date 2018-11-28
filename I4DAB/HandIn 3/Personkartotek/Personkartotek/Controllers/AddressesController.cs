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
    public class AddressesController : ControllerBase
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public AddressesController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the Addresses created
        /// </summary>
        /// <returns>A List of Addresses has been shown</returns>
        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Address> GetAddress()
        {
            return _context.AddressMigration;
        }

        /// <summary>
        /// Gets a specified Address
        /// </summary>
        /// <returns>A specified Address has been shown</returns>
        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.AddressMigration.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        /// <summary>
        /// Updates a specified Address (Creates if it doesn't exist)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "addressId": 0,
        ///         "personId": 0,
        ///         "zipId": 0,
        ///         "streetName": "string",
        ///         "houseNumber": "string",
        ///         "person": ,
        ///         "zip": ,
        ///     }
        /// 
        /// </remarks>
        /// <returns>A specified Address has been updated or a Address has been created</returns>
        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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
        /// Creates an Address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "addressId": 0,
        ///         "personId": 0,
        ///         "zipId": 0,
        ///         "streetName": "string",
        ///         "houseNumber": "string",
        ///         "person": ,
        ///         "zip": ,
        ///     }
        /// 
        /// </remarks>
        /// <returns>A new Address has been created</returns>
        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AddressMigration.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        /// <summary>
        /// Deletes a specified Address
        /// </summary>
        /// <returns>A specified Address has been deleted</returns>
        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.AddressMigration.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.AddressMigration.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        private bool AddressExists(int id)
        {
            return _context.AddressMigration.Any(e => e.AddressId == id);
        }
    }
}