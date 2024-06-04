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
    public class FirmController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _context;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Firm, App.BLL.DTO.Firm> _mapper;

        public FirmController(IAppBLL bll, AppDbContext context, IAppUnitOfWork uow, IMapper autoMapper)
        {
            _bll = bll;
            _context = context;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Firm, App.BLL.DTO.Firm>(autoMapper);
        }

        // GET: api/Firm
        [HttpGet]
        [ProducesResponseType<IEnumerable<Firm>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<Firm>>> GetExercises()
        {
            var res = await _bll.Firms.GetAllAsync();
            return Ok(res);
        }

        // GET: api/Firm/5
        [HttpGet("{id}")]
        [ProducesResponseType<Firm>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Firm>> GetFirm(Guid id)
        {
            var firm = await _bll.Firms.FirstOrDefaultAsync(id);

            if (firm == null)
            {
                return NotFound();
            }

            return _mapper.Map(firm);
        }

        // PUT: api/Firm/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutFirm(Guid id, Firm firm)
        {
            if (id != firm.Id)
            {
                return BadRequest();
            }

            _context.Entry(firm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmExists(id))
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

        // POST: api/Firm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Firm>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Firm>> PostFirm(Firm firm)
        {
            _bll.Firms.Add(_mapper.Map(firm));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetFirm", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = firm.Id
            }, firm);
        }

        // DELETE: api/Firm/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteFirm(Guid id)
        {
            var firm = await _bll.Firms.FirstOrDefaultAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            await _bll.Firms.RemoveAsync(firm);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool FirmExists(Guid id)
        {
            return _bll.Firms.Exists(id);
        }
    }
}
