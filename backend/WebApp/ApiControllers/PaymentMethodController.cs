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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public PaymentMethodController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/PaymentMethod
        [HttpGet]
        [ProducesResponseType<IEnumerable<PaymentMethod>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetExerciseResults()
        {
            var res = await _uow.PaymentMethod.GetAllAsync();
            return Ok(res);
        }

        // GET: api/PaymentMethod/5
        [HttpGet("{id}")]
        [ProducesResponseType<PaymentMethod>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _uow.PaymentMethod.FirstOrDefaultAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return paymentMethod;
        }

        // PUT: api/PaymentMethod/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
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

        // POST: api/PaymentMethod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<PaymentMethod>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            _uow.PaymentMethod.Add(paymentMethod);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = paymentMethod.Id
            }, paymentMethod);
        }

        // DELETE: api/PaymentMethod/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _uow.PaymentMethod.FirstOrDefaultAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            await _uow.PaymentMethod.RemoveAsync(paymentMethod);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentMethodExists(Guid id)
        {
            return _uow.PaymentMethod.Exists(id);
        }
    }
}
