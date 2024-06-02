using System.Net;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DTO.v1_0;
using App.DAL.EF;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ParticipantEventController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _context;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.ParticipantEvent, App.BLL.DTO.ParticipantEvent> _mapper;

        public ParticipantEventController(IAppBLL bll, AppDbContext context, IMapper autoMapper)
        {
            _bll = bll;
            _context = context;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.ParticipantEvent, App.BLL.DTO.ParticipantEvent>(autoMapper);
        }

        // GET: api/ParticipantEvent
        [HttpGet]
        [ProducesResponseType<IEnumerable<ParticipantEvent>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<ParticipantEvent>>> ParticipantEvents()
        {
            var res = await _bll.ParticipantEvents.GetAllAsync(User.GetUserId());
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
            var participantEvent = await _bll.ParticipantEvents.FirstOrDefaultAsync(id, User.GetUserId());
            
            Console.WriteLine("debughere");
            if (participantEvent!.Person == null)
            {
                Console.WriteLine("person is null");
            }
            else
            {
                Console.WriteLine(participantEvent!.Person!.FirstName);
            }
            

            if (participantEvent == null)
            {
                return NotFound();
            }

            return _mapper.Map(participantEvent);
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
            
            var updatedParticipantEvent = _bll.ParticipantEvents.Update(_mapper.Map(participantEvent), User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
                return CreatedAtAction("PutParticipantEvent", new
                {
                    version = HttpContext.GetRequestedApiVersion()?.ToString(),
                    id = updatedParticipantEvent.Id
                }, updatedParticipantEvent);
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
            var updatedParticipantEvent = _bll.ParticipantEvents.AddParticipantToEventAsync(_mapper.Map(participantEvent), User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetParticipantEvent", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = updatedParticipantEvent.Id
            }, updatedParticipantEvent);
        }

        // DELETE: api/ParticipantEvent/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteParticipantEvent(Guid id)
        {
            var participantEvent = await _bll.ParticipantEvents.FirstOrDefaultAsync(id, User.GetUserId());
            if (participantEvent == null)
            {
                return NotFound();
            }

            await _bll.ParticipantEvents.RemoveAsync(participantEvent, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantEventExists(Guid id)
        {
            return _bll.ParticipantEvents.Exists(id, User.GetUserId());
        }
    }
}
