using Cargo4You.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Cargo4You.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuotationsController : Controller
	{
		public Cargo4youContext _context;
		private readonly ILogger<QuotationsController> _logger;

		public QuotationsController(Cargo4youContext context, ILogger<QuotationsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<Quotation>> Get()
		{
			try
			{
				var data = await _context.Quotations.ToListAsync();
				
				return Ok(data);

			} catch(Exception ex)
			{
				_logger.LogError("There was an error getting quotation list data", ex);
				return StatusCode(500, "There was an error getting quotation list data");
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Quotation>> Get(int id)
		{
			try 
			{ 
				var quotation = await _context.Quotations.FindAsync(id);

				if (quotation != null)
				{
					return Ok(quotation);
				}

				return NotFound();
			} catch(Exception ex)
			{
				_logger.LogError("There was an error getting a quotation detail", ex);
				return StatusCode(500, "There was an error getting a quotation detail");
			}
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Quotation data)
		{
			try
			{

				await _context.Quotations.AddAsync(data);
				await _context.SaveChangesAsync();

				int lastQuotationId = _context.Quotations.Max(item => item.QuotationId);

				return Ok(lastQuotationId);
			} catch(Exception ex)
			{
				_logger.LogError("There was an error getting quotation list data", ex);
				return StatusCode(500, "Could not store quotation data");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] Quotation data)
		{
			try
			{
				var quotation = await _context.Quotations.FindAsync(id);

				if (quotation == null)
					return BadRequest();

				if (data.CourierId == null)
					data.CourierId = quotation.CourierId;

				if (data.ClientName == null)
					data.ClientName = quotation.ClientName;

				if (data.Width == null)
					data.Width = quotation.Width;

				if (data.Height == null)
					data.Height = quotation.Height;

				if (data.Depth == null)
					data.Depth = quotation.Depth;

				if (data.Weight == null)
					data.Weight = quotation.Weight;

				if (data.Price == null)
					data.Price = quotation.Price;

				data.QuotationId = quotation.QuotationId;
				_context.Entry(quotation).CurrentValues.SetValues(data);
				await _context.SaveChangesAsync();

				return Ok("Quotation updated successfully");
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			
			var quotation = await _context.Quotations.FindAsync(id);

			if (quotation == null)
				throw new InvalidOperationException("Review cannot be found");

			_context.Quotations.Remove(quotation);
			await _context.SaveChangesAsync();

			return Ok();
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
