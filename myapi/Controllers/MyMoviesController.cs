using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public MyMoviesController(IMyMovieService myMovieService,
            IMyMovieDetailService myMovieDetailService,
            ILogger<MyMoviesController> logger)
        {
            _myMovieService = myMovieService;
            _myMovieDetailService = myMovieDetailService;
            _logger = logger;
        }
        // GET: api/MyMovies
        [HttpGet]
        public ActionResult<string> Get()
        {
            IEnumerable<MovieItem> results = _myMovieService.GetMovies();
            MoviesDTO responseBody = new MoviesDTO
            {
                Movies = results.ToList()
            };
            return JsonConvert.SerializeObject(responseBody);
        }

        // GET: api/MyMovies/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<string> Get(string id)
        {
            _logger.LogInformation("*********************ID Requested: " + id);
            MovieDetail result = _myMovieDetailService.GetDetailById(id);
            _logger.LogInformation(JsonConvert.SerializeObject(result));
            return JsonConvert.SerializeObject(result);
            
        }

    }
}
