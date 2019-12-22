using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using myapi.Models;
using myapi.Services;
using Newtonsoft.Json;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMoviesController : ControllerBase
    {
        private readonly ILogger<MyMoviesController> _logger;
        private readonly IMyMovieService _myMovieService;
        private readonly IMyMovieDetailService _myMovieDetailService;
        private readonly IMemoryCache _memoryCahce;
        public MyMoviesController(IMyMovieService myMovieService,
            IMyMovieDetailService myMovieDetailService,
            ILogger<MyMoviesController> logger,
            IMemoryCache memoryCache)
        {
            _myMovieService = myMovieService;
            _myMovieDetailService = myMovieDetailService;
            _logger = logger;
            _memoryCahce = memoryCache;
        }
        // GET: api/MyMovies
        [HttpGet]
        public ActionResult<string> Get()
        {
            var cacheKey = "movieList";
            if (_memoryCahce.TryGetValue(cacheKey, out string results))
            {
                _logger.LogInformation("***************cache hit for list");
                return Ok(results);
            }
            else
            {
                var resultCollection = _myMovieService.GetMovies();
                if(resultCollection.Any())
                {
                    MoviesDTO responseBody = new MoviesDTO
                    {
                        Movies = resultCollection.ToList()
                    };
                    var resultJson = JsonConvert.SerializeObject(responseBody);
                    _memoryCahce.Set(cacheKey, resultJson);
                    return Ok(resultJson);
                }
                else
                {
                    return NotFound();
                }
               
            }

        }

        // GET: api/MyMovies/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<string> Get(string id)
        {
            var cacheKey = "movieDetail_" + id;
            if (_memoryCahce.TryGetValue(cacheKey, out string resultStr))
            {
                _logger.LogInformation("***************cache hit for detail " + id);
                return Ok(resultStr);
            }
            else
            {
                var result = _myMovieDetailService.GetDetailById(id);
                if(result != null)
                {
                    var resultJson = JsonConvert.SerializeObject(result);
                    _memoryCahce.Set(cacheKey, resultJson);
                    return Ok(resultJson);
                }
                else
                {
                    return NotFound();
                }
                
            }           
        }

    }
}
