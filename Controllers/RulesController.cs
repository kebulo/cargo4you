using Cargo4You.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo4You.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RulesController : Controller
	{
		public Cargo4youContext _context;
		private readonly ILogger<QuotationsController> _logger;

		public RulesController(Cargo4youContext context, ILogger<QuotationsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<Rule>> Get([FromQuery] int? courier_id, bool? is_dimension, int? min, string? max)
		{
			try
			{
				//.Where((rule) =>
				//{
				// if (is_dimension != null && (bool)is_dimension)
				// return rule.IsDimension == (bool)is_dimension;
				//if (courier_id != null)
				// return rule.CourierId == courier_id;
				//if (min != null)
				//return rule.Min == min;
				//if (max != null)
				// return rule.Max == max;
				//})
				var data = await _context.Rules.Join(
					_context.Couriers,
					rule => rule.CourierId,
					cou => cou.CourierId,
					(rule, cou) => new
					{
						PriceId = rule.PriceId,
						CourierId = rule.CourierId,
						Price = rule.Price,
						IsDimension = rule.IsDimension,
						Min = rule.Min,
						ExtraKg = rule.ExtraKg,
						Max = rule.Max,
						ExtraValue = rule.ExtraValue,
						CourierName = cou.Name
					}).Select(x => new Rule()
					{
						PriceId = x.PriceId,
						CourierId = x.CourierId,
						Price = x.Price,
						IsDimension = x.IsDimension,
						Min = x.Min,
						ExtraKg = x.ExtraKg,
						Max = x.Max,
						ExtraValue = x.ExtraValue,
						CourierName = x.CourierName
					}).ToListAsync();

				return Ok(data);

			}
			catch (Exception ex)
			{
				_logger.LogError("There was an error getting rule list data", ex);
				return StatusCode(500, "There was an error getting rule list data");
			}
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
