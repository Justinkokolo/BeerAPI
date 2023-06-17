using Beers.Models;
using Microsoft.AspNetCore.Mvc;
using Beers.Services;


namespace Beers.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly IPunkApiService _punkApiService;

        public BeerController(IPunkApiService punkApiService)
        {
            _punkApiService = punkApiService;
        }

        [HttpGet("menu")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetMenu()
        {
             var beers = await _punkApiService.GetMenu();
            
             return Ok(beers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeerById(int id)
        {
            var beer = await _punkApiService.GetBeerById(id);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }

        [HttpGet("random")]
        public async Task<ActionResult<Beer>> GetRandomBeer()
        {
            var beer = await _punkApiService.GetRandomBeer();
            return Ok(beer);
        }

        [HttpGet("/search")]
        public async Task<ActionResult<IEnumerable<Beer>>> SearchBeers([FromQuery] string query)
        {
            var beers = await _punkApiService.SearchBeers(query);
            return Ok(beers);
        }
    }

}
