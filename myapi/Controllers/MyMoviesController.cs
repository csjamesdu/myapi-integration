using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using myapi.Services;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMoviesController : ControllerBase
    {
        private readonly ILogger<MyMoviesController> _logger;
        private readonly IMyMovieDetailService _myMovieDetailService;
        private readonly IMemoryCache _memoryCahce;

        private readonly IDataAggregationService _dataAggregationService;

        public MyMoviesController(
            IMyMovieDetailService myMovieDetailService,
            ILogger<MyMoviesController> logger,
            IMemoryCache memoryCache,
            IDataAggregationService dataAggregationService)
        {
            _myMovieDetailService = myMovieDetailService;
            _logger = logger;
            _memoryCahce = memoryCache;
            _dataAggregationService = dataAggregationService;
        }
        // GET: api/MyMovies
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var resultDTO = await _dataAggregationService.GetMovieList();
            if (resultDTO != null)
            {
                return Ok(resultDTO);
            }
            else
            {
                return NotFound();
            }

        }

        // GET: api/MyMovies/5
        [HttpGet("{id}", Name = "GetDetails")]
        public async Task<ActionResult<string>> Get(string id)
        {
            var resultStr = await _dataAggregationService.GetMovieDetail(id);
            if (resultStr != null)
            {
                return Ok(resultStr);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
