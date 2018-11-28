using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Models;

namespace Personkartotek.Controllers
{
    public class PeopleAppController : Controller
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public PeopleAppController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        // GET: PeopleApp
        public async Task<IActionResult> Index()
        {
            return View(await _context.PersonMigration.ToListAsync());
        }

        // GET: PeopleApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.PersonMigration
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: PeopleApp/Create
        public IActionResult Create()
        {
            return View();
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
        /// <returns>A Newly</returns>
        // POST: PeopleApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,PersonType,FirstName,MiddleName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PeopleApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.PersonMigration.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: PeopleApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,PersonType,FirstName,MiddleName,LastName")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PeopleApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.PersonMigration
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PeopleApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.PersonMigration.FindAsync(id);
            _context.PersonMigration.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.PersonMigration.Any(e => e.PersonId == id);
        }
    }
}
