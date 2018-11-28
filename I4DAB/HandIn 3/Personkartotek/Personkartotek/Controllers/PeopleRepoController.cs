using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Interfaces;
using Personkartotek.Models;
using Personkartotek.Repositories;

namespace Personkartotek.Controllers
{
    public class PeopleRepoController : Controller
    {
        //private readonly PersonkartotekDBHandIn32Context _context;

        private readonly IPersonRepo _personRepo;

        //public PeopleRepoController(PersonkartotekDBHandIn32Context context)
        //{
        //    _context = context;
        //}

        public PeopleRepoController(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }

        // GET: PeopleRepo
        public IActionResult Index()
        {
            var people = from entity in _personRepo.GetAll()
                         select entity;
            return View(people);
        }

        // GET: PeopleRepo/Details/5
        public IActionResult Details(int id)
        {
            Person person = _personRepo.Get(id);

            return View(person);
        }

        // GET: PeopleRepo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeopleRepo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PersonId,PersonType,FirstName,MiddleName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepo.Add(person);
                _personRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PeopleRepo/Edit/5
        public IActionResult Edit(int id)
        {
            Person person = _personRepo.Get(id);
            return View(person);
        }

        // POST: PeopleRepo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PersonId,PersonType,FirstName,MiddleName,LastName")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _personRepo.Update(person);
                _personRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PeopleRepo/Delete/5
        public IActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Unable to save changes. Try again.";
            }

            Person person = _personRepo.Get(id);
            return View(person);
        }

        // POST: PeopleRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            Person person = _personRepo.Get(id);
            _personRepo.Remove(person);
            _personRepo.Save();
            
            return RedirectToAction(nameof(Index));
        }

        //private bool PersonExists(int id)
        //{
        //    return _personRepo.GetAll(id);
        //}
    }
}
