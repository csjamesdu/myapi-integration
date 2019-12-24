using myapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class FilmWorldAPIService : IFilmWorldAPIService
    {
        const string FILM_WORLD_LIST_API = "api/filmworld/movies";
        const string FILM_WORLD_DETAIL_API = "api/filmworld/movie/fw";
        const string FILM_WORLD_CLIENT = "MyMovieClient";

        private readonly IMovieAPIUtilService _movieAPIUtilService;

        public FilmWorldAPIService(IMovieAPIUtilService movieAPIUtilService)
        {
            _movieAPIUtilService = movieAPIUtilService;
        }

        public async Task<IEnumerable<MovieItem>> AsycGetMovieList()
        {
            var result = await _movieAPIUtilService.GetMovieListFromAPI(FILM_WORLD_LIST_API, FILM_WORLD_CLIENT);
            return result;
        }

        public async Task<MovieDetail> AsyncGetMovieDetail(string id)
        {
            var result = await _movieAPIUtilService.GetMovieDetailFromAPI(FILM_WORLD_DETAIL_API + id, FILM_WORLD_CLIENT);
            return result;
        }


    }
}
