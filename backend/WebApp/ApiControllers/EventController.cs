using System.Net;
using System.Security.Claims;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DTO.v1_0;
using App.DAL.EF;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _context;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Event, App.BLL.DTO.Event> _mapper;

        public EventController(IAppBLL bll, AppDbContext context, IMapper autoMapper)
        {
            _bll = bll;
            _context = context;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Event, App.BLL.DTO.Event>(autoMapper);
        }

        // GET: api/Event
        [HttpGet]
        [ProducesResponseType<IEnumerable<Event>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var res = await _bll.Events.GetAllAsync(User.GetUserId());
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
            var @event = await _bll.Events.FirstOrDefaultAsync(id, User.GetUserId());

            if (@event == null)
            {
                return NotFound();
            }

            return _mapper.Map(@event);
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "id_policy")]
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
        [Authorize(Policy = "id_policy")]
        [HttpPost]
        [ProducesResponseType<Event>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _bll.Events.Add(_mapper.Map(@event), User.GetUserId());
            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetEvent", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = @event.Id
            }, @event);
        }

        // DELETE: api/Event/5
        [Authorize(Policy = "id_policy")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _bll.Events.FirstOrDefaultAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            

            foreach (var pe in (await _bll.ParticipantEvents.GetAllByEventId(@event.Id, User.GetUserId()))!)
            {
                await _bll.ParticipantEvents.RemoveAsync(pe);
            }

            await _bll.Events.RemoveAsync(@event);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _bll.Events.Exists(id, User.GetUserId());
        }
    }
}
