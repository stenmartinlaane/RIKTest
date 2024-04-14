using System.Net;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.DTO;
using Asp.Versioning;

namespace WebApp.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public EventController(IAppUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        // GET: api/Event
        [HttpGet]
        [ProducesResponseType<IEnumerable<Event>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAccounts()
        {
            var res = await _uow.Event.GetAllAsync();
            return Ok(res);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        [ProducesResponseType<Event>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _uow.Event.FirstOrDefaultAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Event>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _uow.Event.Add(@event);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = @event.Id
            }, @event);
        }

        // DELETE: api/Event/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _uow.Event.FirstOrDefaultAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            await _uow.Event.RemoveAsync(@event);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _uow.Event.Exists(id);
        }
    }
}
