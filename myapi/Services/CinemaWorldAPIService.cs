using myapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class CinemaWorldAPIService : ICinemaWorldAPIService
    {
        const string CINEMA_WORLD_LIST_API = "api/cinemaworld/movies";
        const string CINEMA_WORLD_DETAIL_API = "api/cinemaworld/movie/cw";
        const string CINEMA_WORLD_CLIENT = "MyMovieClient";

        private readonly IMovieAPIUtilService _movieAPIUtilService;

        public CinemaWorldAPIService(IMovieAPIUtilService movieAPIUtilService)
        {
            _movieAPIUtilService = movieAPIUtilService;
        }

        public async Task<IEnumerable<MovieItem>> AsycGetMovieList()
        {
            var result = await _movieAPIUtilService.GetMovieListFromAPI(CINEMA_WORLD_LIST_API, CINEMA_WORLD_CLIENT);
            return result;
        }

        public async Task<MovieDetail> AsyncGetMovieDetail(string id)
        {
            var result = await _movieAPIUtilService.GetMovieDetailFromAPI(CINEMA_WORLD_DETAIL_API + id, CINEMA_WORLD_CLIENT);
            return result;
        }

    }
}
