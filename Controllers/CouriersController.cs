using Cargo4You.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo4You.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CouriersController : Controller
	{
		public Cargo4youContext _context;
		private readonly ILogger<QuotationsController> _logger;

		public CouriersController(Cargo4youContext context, ILogger<QuotationsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<Courier>> Get()
		{
			try
			{
				var data = await _context.Couriers.ToListAsync();
				
				return Ok(data);
			} catch(Exception ex)
			{
				_logger.LogError("There was an error getting quotation list data", ex);
				return StatusCode(500, "There was an error getting quotation list data");
			}
			
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
