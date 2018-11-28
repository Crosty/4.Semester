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
    public class EmailsAppController : Controller
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public EmailsAppController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        // GET: EmailsApp
        public async Task<IActionResult> Index()
        {
            var personkartotekDBHandIn32Context = _context.EmailMigration.Include(e => e.Person);
            return View(await personkartotekDBHandIn32Context.ToListAsync());
        }

        // GET: EmailsApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.EmailMigration
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: EmailsApp/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName");
            return View();
        }

        // POST: EmailsApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,PersonId,EmailAddress")] Email email)
        {
            if (ModelState.IsValid)
            {
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", email.PersonId);
            return View(email);
        }

        // GET: EmailsApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.EmailMigration.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", email.PersonId);
            return View(email);
        }

        // POST: EmailsApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmailId,PersonId,EmailAddress")] Email email)
        {
            if (id != email.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.EmailId))
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
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", email.PersonId);
            return View(email);
        }

        // GET: EmailsApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.EmailMigration
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: EmailsApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var email = await _context.EmailMigration.FindAsync(id);
            _context.EmailMigration.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
            return _context.EmailMigration.Any(e => e.EmailId == id);
        }
    }
}
