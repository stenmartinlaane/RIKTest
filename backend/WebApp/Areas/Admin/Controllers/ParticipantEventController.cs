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
    public class ParticipantEventController : Controller
    {
        private readonly AppDbContext _context;

        public ParticipantEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ParticipantEvent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ParticipantEvents.Include(p => p.Event).Include(p => p.Firm).Include(p => p.PaymentMethod).Include(p => p.Person);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ParticipantEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents
                .Include(p => p.Event)
                .Include(p => p.Firm)
                .Include(p => p.PaymentMethod)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "AdditionalInformation");
            ViewData["FirmId"] = new SelectList(_context.Firms, "Id", "CreatedBy");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "CreatedBy");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "CreatedBy");
            return View();
        }

        // POST: Admin/ParticipantEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegisterDateTime,PersonId,FirmId,EventId,AdditionalNotes,PaymentMethodId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ParticipantEvent participantEvent)
        {
            if (ModelState.IsValid)
            {
                participantEvent.Id = Guid.NewGuid();
                _context.Add(participantEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "AdditionalInformation", participantEvent.EventId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "Id", "CreatedBy", participantEvent.FirmId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "CreatedBy", participantEvent.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "CreatedBy", participantEvent.PersonId);
            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents.FindAsync(id);
            if (participantEvent == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "AdditionalInformation", participantEvent.EventId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "Id", "CreatedBy", participantEvent.FirmId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "CreatedBy", participantEvent.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "CreatedBy", participantEvent.PersonId);
            return View(participantEvent);
        }

        // POST: Admin/ParticipantEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RegisterDateTime,PersonId,FirmId,EventId,AdditionalNotes,PaymentMethodId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ParticipantEvent participantEvent)
        {
            if (id != participantEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participantEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantEventExists(participantEvent.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "AdditionalInformation", participantEvent.EventId);
            ViewData["FirmId"] = new SelectList(_context.Firms, "Id", "CreatedBy", participantEvent.FirmId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "CreatedBy", participantEvent.PaymentMethodId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "CreatedBy", participantEvent.PersonId);
            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents
                .Include(p => p.Event)
                .Include(p => p.Firm)
                .Include(p => p.PaymentMethod)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            return View(participantEvent);
        }

        // POST: Admin/ParticipantEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var participantEvent = await _context.ParticipantEvents.FindAsync(id);
            if (participantEvent != null)
            {
                _context.ParticipantEvents.Remove(participantEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantEventExists(Guid id)
        {
            return _context.ParticipantEvents.Any(e => e.Id == id);
        }
    }
}
