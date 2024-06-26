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
    public class FirmController : Controller
    {
        private readonly AppDbContext _context;

        public FirmController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Firm
        public async Task<IActionResult> Index()
        {
            return View(await _context.Firms.ToListAsync());
        }

        // GET: Admin/Firm/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // GET: Admin/Firm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Firm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,RegistryCode,ParticipantCount,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Firm firm)
        {
            if (ModelState.IsValid)
            {
                firm.Id = Guid.NewGuid();
                _context.Add(firm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firm);
        }

        // GET: Admin/Firm/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms.FindAsync(id);
            if (firm == null)
            {
                return NotFound();
            }
            return View(firm);
        }

        // POST: Admin/Firm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,RegistryCode,ParticipantCount,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Firm firm)
        {
            if (id != firm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmExists(firm.Id))
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
            return View(firm);
        }

        // GET: Admin/Firm/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firm = await _context.Firms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // POST: Admin/Firm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm != null)
            {
                _context.Firms.Remove(firm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirmExists(Guid id)
        {
            return _context.Firms.Any(e => e.Id == id);
        }
    }
}
