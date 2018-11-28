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
    public class AddressesAppController : Controller
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public AddressesAppController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        // GET: AddressesApp
        public async Task<IActionResult> Index()
        {
            var personkartotekDBHandIn32Context = _context.AddressMigration.Include(a => a.Person).Include(a => a.Zip);
            return View(await personkartotekDBHandIn32Context.ToListAsync());
        }

        // GET: AddressesApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.AddressMigration
                .Include(a => a.Person)
                .Include(a => a.Zip)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: AddressesApp/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName");
            ViewData["ZipId"] = new SelectList(_context.ZipMigration, "ZipId", "City");
            return View();
        }

        // POST: AddressesApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,PersonId,ZipId,StreetName,HouseNumber")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", address.PersonId);
            ViewData["ZipId"] = new SelectList(_context.ZipMigration, "ZipId", "City", address.ZipId);
            return View(address);
        }

        // GET: AddressesApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.AddressMigration.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", address.PersonId);
            ViewData["ZipId"] = new SelectList(_context.ZipMigration, "ZipId", "City", address.ZipId);
            return View(address);
        }

        // POST: AddressesApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,PersonId,ZipId,StreetName,HouseNumber")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
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
            ViewData["PersonId"] = new SelectList(_context.PersonMigration, "PersonId", "FirstName", address.PersonId);
            ViewData["ZipId"] = new SelectList(_context.ZipMigration, "ZipId", "City", address.ZipId);
            return View(address);
        }

        // GET: AddressesApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.AddressMigration
                .Include(a => a.Person)
                .Include(a => a.Zip)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: AddressesApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.AddressMigration.FindAsync(id);
            _context.AddressMigration.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.AddressMigration.Any(e => e.AddressId == id);
        }
    }
}
