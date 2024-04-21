using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Http;
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
    public class ParticipantEventController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public ParticipantEventController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/ParticipantEvent
        [HttpGet]
        [ProducesResponseType<IEnumerable<ParticipantEvent>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<ParticipantEvent>>> ParticipantEvents()
        {
            var res = await _uow.ParticipantEvent.GetAllAsync();
            return Ok(res);
        }

        // GET: api/ParticipantEvent/5
        [HttpGet("{id}")]
        [ProducesResponseType<ParticipantEvent>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ParticipantEvent>> GetParticipantEvent(Guid id)
        {
            var participantEvent = await _uow.ParticipantEvent.FirstOrDefaultAsync(id);

            if (participantEvent == null)
            {
                return NotFound();
            }

            return participantEvent;
        }

        // PUT: api/ParticipantEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutParticipantEvent(Guid id, ParticipantEvent participantEvent)
        {
            if (id != participantEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(participantEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantEventExists(id))
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

        // POST: api/ParticipantEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<ParticipantEvent>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ParticipantEvent>> PostParticipantEvent(ParticipantEvent participantEvent)
        {
            _uow.ParticipantEvent.Add(participantEvent);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetParticipantEvent", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = participantEvent.Id
            }, participantEvent);
        }

        // DELETE: api/ParticipantEvent/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteParticipantEvent(Guid id)
        {
            var participantEvent = await _uow.ParticipantEvent.FirstOrDefaultAsync(id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            await _uow.ParticipantEvent.RemoveAsync(participantEvent);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantEventExists(Guid id)
        {
            return _uow.ParticipantEvent.Exists(id);
        }
    }
}
