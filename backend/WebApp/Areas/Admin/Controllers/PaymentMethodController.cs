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
    public class PaymentMethodController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentMethodController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PaymentMethod
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerciseResults.ToListAsync());
        }

        // GET: Admin/PaymentMethod/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.ExerciseResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PaymentMethod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MethodName,MethodDescription,Active,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                paymentMethod.Id = Guid.NewGuid();
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethod/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.ExerciseResults.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: Admin/PaymentMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MethodName,MethodDescription,Active,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.Id))
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
            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethod/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.ExerciseResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: Admin/PaymentMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentMethod = await _context.ExerciseResults.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.ExerciseResults.Remove(paymentMethod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(Guid id)
        {
            return _context.ExerciseResults.Any(e => e.Id == id);
        }
    }
}
