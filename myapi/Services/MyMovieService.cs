using Microsoft.Extensions.Logging;
using myapi.DAO;
using myapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class MyMovieService : IMyMovieService
    {
        const string FILM_WORLD_API = "api/filmworld/movies"; 
        const string CINEMA_WORLD_API = "api/cinemaworld/movies";

        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly ILogger<MyMovieService> _logger;
        private readonly IMovieItemDAO _movieItemDAO;

        public MyMovieService(IHttpClientFactory httpClientFactory, 
            ILogger<MyMovieService> logger,
            IMovieItemDAO movieItemDAO) 
        { 
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _movieItemDAO = movieItemDAO;
        }
        public async Task<IEnumerable<MovieItem>> GetMovies()
        {
            var result = _movieItemDAO.GetMovieItemsFromInMemDB(); 
            if (result == null || !result.Any()) 
            {   
                result = await GetMovieItemsFromEitherAPI();
                _movieItemDAO.SaveMovieItemsToInMemDB(result); 
                _logger.LogInformation("***************Result Saved."); 
            }

            _logger.LogInformation(JsonConvert.SerializeObject(result.ToList()));

            return result;

        }

        private async Task<IEnumerable<MovieItem>> GetMovieItemsFromEitherAPI()
        {
            var resultCW = await GetMoviesFromProvider(CINEMA_WORLD_API);
            var resultFW = await GetMoviesFromProvider(FILM_WORLD_API);

            return MergeAndProcessResults(resultCW, resultFW);
        }

        private IEnumerable<MovieItem> MergeAndProcessResults(IEnumerable<MovieItem> original, IEnumerable<MovieItem> others)
        {
            var interimResults = original.Concat(others);
            foreach (MovieItem item in interimResults)
            {
                item.ID = item.ID.Substring(2);
                item.Poster = PosterResources.PosterDic[item.ID];
            }
            var finalResult = interimResults
                .ToList().GroupBy(item => item.ID).Select(group => group.First());
            return finalResult;
        }


        private async Task<List<MovieItem>> GetMoviesFromProvider(string provider)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("MyMovieClient");
                var request = new HttpRequestMessage(HttpMethod.Get, provider);
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();
                var movies = JsonConvert.DeserializeObject<MoviesDTO>(await response.Content.ReadAsStringAsync());
                var itemList = movies.Movies;
                return itemList;
            }
            catch(Exception e)
            {
                _logger.LogError(provider + " API Fails: " + e);
                return new List<MovieItem>();
            }
        }
    }
}
