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
    public class ZipsAppController : Controller
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public ZipsAppController(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
        }

        // GET: ZipsApp
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZipMigration.ToListAsync());
        }

        // GET: ZipsApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zip = await _context.ZipMigration
                .FirstOrDefaultAsync(m => m.ZipId == id);
            if (zip == null)
            {
                return NotFound();
            }

            return View(zip);
        }

        // GET: ZipsApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZipsApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZipId,City,Country,ZipCode")] Zip zip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zip);
        }

        // GET: ZipsApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zip = await _context.ZipMigration.FindAsync(id);
            if (zip == null)
            {
                return NotFound();
            }
            return View(zip);
        }

        // POST: ZipsApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZipId,City,Country,ZipCode")] Zip zip)
        {
            if (id != zip.ZipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZipExists(zip.ZipId))
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
            return View(zip);
        }

        // GET: ZipsApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zip = await _context.ZipMigration
                .FirstOrDefaultAsync(m => m.ZipId == id);
            if (zip == null)
            {
                return NotFound();
            }

            return View(zip);
        }

        // POST: ZipsApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zip = await _context.ZipMigration.FindAsync(id);
            _context.ZipMigration.Remove(zip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZipExists(int id)
        {
            return _context.ZipMigration.Any(e => e.ZipId == id);
        }
    }
}
