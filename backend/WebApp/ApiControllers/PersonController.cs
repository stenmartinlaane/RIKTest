using System.Net;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.EF;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Person
        [HttpGet]
        [ProducesResponseType<IEnumerable<Person>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<Person>>> GetGames()
        {
            var res = await _uow.Event.GetAllAsync();
            return Ok(res);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        [ProducesResponseType<Person>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _uow.Person.FirstOrDefaultAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Person>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _uow.Person.Add(person);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = person.Id
            }, person);
        }

        // DELETE: api/Person/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _uow.Person.FirstOrDefaultAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _uow.Person.RemoveAsync(person);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _uow.Person.Exists(id);
        }
    }
}
