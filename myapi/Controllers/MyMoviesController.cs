using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapi.Models;
using myapi.Services;
using Newtonsoft.Json;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMoviesController : ControllerBase
    {
        private readonly IMyMovieService _myMovieService;
        public MyMoviesController(IMyMovieService movieService)
        {
            _myMovieService = movieService;
        }
        // GET: api/MyMovies
        [HttpGet]
        public ActionResult<string> Get()
        {
            IEnumerable<MovieItem> results = _myMovieService.getMovies();
            MoviesDTO responseBody = new MoviesDTO
            {
                Movies = results.ToList()
            };
            return JsonConvert.SerializeObject(responseBody);
        }

        // GET: api/MyMovies/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
