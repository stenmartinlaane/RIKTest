using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Person
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Games.Include(p => p.PaymentMethod);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Person/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Games
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Admin/Person/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.ExerciseResults, "Id", "CreatedBy");
            return View();
        }

        // POST: Admin/Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PersonalIdentificationNumber,AdditionalNotes,PaymentMethodId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.ExerciseResults, "Id", "CreatedBy", person.PaymentMethodId);
            return View(person);
        }

        // GET: Admin/Person/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Games.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.ExerciseResults, "Id", "CreatedBy", person.PaymentMethodId);
            return View(person);
        }

        // POST: Admin/Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,PersonalIdentificationNumber,AdditionalNotes,PaymentMethodId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Person person)
        {
            if (id != person.Id)
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
                    if (!PersonExists(person.Id))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.ExerciseResults, "Id", "CreatedBy", person.PaymentMethodId);
            return View(person);
        }

        // GET: Admin/Person/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Games
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Admin/Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var person = await _context.Games.FindAsync(id);
            if (person != null)
            {
                _context.Games.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(Guid id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
