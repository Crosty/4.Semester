using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using quickstartcore.Models;

namespace quickstartcore.Controllers
{
    [Produces("application/json")]
    [Route("api/PersonAPI")]
    public class PersonAPIController : Controller
    {
        private readonly IDocumentDBRepository<Person> _repository;
        public PersonAPIController(IDocumentDBRepository<Person> repository)
        {
            this._repository = repository;
        }
        // GET: api/PersonAPI
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var items = await _repository.GetItemsAsync();
            return items;
        }

        // GET: api/PersonAPI/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Person item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/PersonAPI
        [HttpPost]
        public async Task<ActionResult> Post([Bind("PersonId,PersonType,FirstName,MiddleName,LastName, Address, Email")] Person person)
        {

            if (ModelState.IsValid)
            {
                var newPerson = await _repository.CreateItemAsync(person);
                return Ok((Person)(dynamic)newPerson);
            }

            return View(person);
        }

        // PUT: api/PersonAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]  string id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != person.PersonId)
            {
                return BadRequest();
            }
            try
            {
                await _repository.UpdateItemAsync(id, person);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound();
                else throw;
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Person person = await _repository.GetItemAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
