 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IIETA.Data;
using IIETA.Models;

namespace IIETA.Controllers
{
    public class handleEmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public handleEmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: handleEmails
        public async Task<IActionResult> Index()
        {
              return _context.handleEmail != null ? 
                          View(await _context.handleEmail.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.handleEmail'  is null.");
        }

        // GET: handleEmails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.handleEmail == null)
            {
                return NotFound();
            }

            var handleEmail = await _context.handleEmail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handleEmail == null)
            {
                return NotFound();
            }

            return View(handleEmail);
        }

        // GET: handleEmails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: handleEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Message")] handleEmail handleEmail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(handleEmail);
                await _context.SaveChangesAsync();

                // Add a success message to TempData
                TempData["SuccessMessage"] = "Email has been successfully sent!";

                // Reset the form fields
                ModelState.Clear();
                handleEmail = new handleEmail(); // Create a new instance to clear the input fields


            }
            return View(handleEmail);
        }

        // GET: handleEmails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.handleEmail == null)
            {
                return NotFound();
            }

            var handleEmail = await _context.handleEmail.FindAsync(id);
            if (handleEmail == null)
            {
                return NotFound();
            }
            return View(handleEmail);
        }

        // POST: handleEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Message")] handleEmail handleEmail)
        {
            if (id != handleEmail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handleEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!handleEmailExists(handleEmail.Id))
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
            return View(handleEmail);
        }

        // GET: handleEmails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.handleEmail == null)
            {
                return NotFound();
            }

            var handleEmail = await _context.handleEmail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (handleEmail == null)
            {
                return NotFound();
            }

            return View(handleEmail);
        }

        // POST: handleEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.handleEmail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.handleEmail'  is null.");
            }
            var handleEmail = await _context.handleEmail.FindAsync(id);
            if (handleEmail != null)
            {
                _context.handleEmail.Remove(handleEmail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool handleEmailExists(int id)
        {
          return (_context.handleEmail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
