using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IIETA.Data;
using IIETA.Models;
using Microsoft.AspNetCore.Authorization;

namespace IIETA.Controllers
{
    public class CatigoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatigoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catigories
        public async Task<IActionResult> Index()
        {
              return _context.Catigorie != null ? 
                          View(await _context.Catigorie.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Catigorie'  is null.");
        }

        public async Task<IActionResult> Admin()
        {
            return _context.Catigorie != null ?
                        View(await _context.Catigorie.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Catigorie'  is null.");
        }


        // GET: Catigories/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catigorie == null)
            {
                return NotFound();
            }

            var catigorie = await _context.Catigorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catigorie == null)
            {
                return NotFound();
            }

            return View(catigorie);
        }

        // GET: Catigories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catigories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type,Location,Total_cost,Act_cost,Is_agent")] Catigorie catigorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catigorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catigorie);
        }




        // GET: Catigories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catigorie = await _context.Catigorie
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();

            if (catigorie == null)
            {
                return NotFound();
            }
            return View(catigorie);
        }

        // POST: Catigories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type,Location,Total_cost,Act_cost,Is_agent")] Catigorie catigorie, int additionalCost)
        {
            if (catigorie == null)
            {
                return NotFound();
            }

/*            if (additionalCost < 0 )
            {
                ModelState.AddModelError("AdditionalCost", "Additional Cost must be a non-negative number.");
   }
            if (additionalCost > catigorie.Act_cost)
            {
                ModelState.AddModelError("AdditionalCost", "Additional Cost must be less than total.");         
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    catigorie.Act_cost += additionalCost; 

                    _context.Update(catigorie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatigorieExists(catigorie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(nameof(Index));
        }




        // GET: Catigories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catigorie == null)
            {
                return NotFound();
            }

            var catigorie = await _context.Catigorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catigorie == null)
            {
                return NotFound();
            }

            return View(catigorie);
        }

        // POST: Catigories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catigorie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Catigorie'  is null.");
            }
            var catigorie = await _context.Catigorie.FindAsync(id);
            if (catigorie != null)
            {
                _context.Catigorie.Remove(catigorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatigorieExists(int id)
        {
          return (_context.Catigorie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
