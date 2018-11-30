using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using quickstartcore.Models;

namespace quickstartcore.Controllers
{
    public class PersonController : Controller
    {
        private readonly IDocumentDBRepository<Person> _repository;
        public PersonController(IDocumentDBRepository<Person> repository)
        {
            this._repository = repository;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            var people = await _repository.GetItemsAsync();
            return View(people);
        }


#pragma warning disable 1998
        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }
#pragma warning restore 1998

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("personId, PersonType, FirstName, MiddleName, LastName, Address, Email")] Person item)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id, PersonType, FirstName, MiddleName, LastName, Address, Email")] Person item)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
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

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
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

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _repository.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            Person item = await _repository.GetItemAsync(id);
            return View(item);
        }
    }
}