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
        public IEnumerable<MovieItem> GetMovies()
        {
            var result = _movieItemDAO.GetMovieItemsFromInMemDB(); 
            if (result == null || !result.Any()) 
            {   
                result = SyncGetMovieItemsByAPI();
                _movieItemDAO.SaveMovieItemsToInMemDB(result); 
                _logger.LogInformation("Result Saved."); 
            }

            _logger.LogInformation(JsonConvert.SerializeObject(result.ToList()));

            return result;

        }
        public IEnumerable<MovieItem> SyncGetMovieItemsByAPI()
        {
            try
            {
                IEnumerable<MovieItem> result = Task
                    .Run(() => AsyncGetMovieItemsByAPI())
                    .GetAwaiter().GetResult();
                return result;
            } catch( Exception e)
            {
                _logger.LogWarning("API Fails" + e); 
                return null;
            }
        }
        public async Task<IEnumerable<MovieItem>> AsyncGetMovieItemsByAPI()
        {
            List<MovieItem> FWMovies = await GetMoviesFromProvider(FILM_WORLD_API); 
            List<MovieItem> CWMovies = await GetMoviesFromProvider(CINEMA_WORLD_API);
            var rawResults = await Task.WhenAll(GetMoviesFromProvider(FILM_WORLD_API), GetMoviesFromProvider(CINEMA_WORLD_API)); 
            var intermediateResult = rawResults.SelectMany(result => result); 
            foreach (MovieItem item in intermediateResult) 
            { 
                item.ID = item.ID.Substring(2); 
                item.Poster = item.Poster.Replace("http", "https"); 
            }
            IEnumerable<MovieItem> finalResult = intermediateResult.ToList().GroupBy(item => item.ID).Select(group => group.First());
            return finalResult;
        }


        private async Task<List<MovieItem>> GetMoviesFromProvider(string provider)
        {
            var httpClient = _httpClientFactory.CreateClient("MyMovieClient"); 
            var request = new HttpRequestMessage(HttpMethod.Get, provider);
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead); 
            response.EnsureSuccessStatusCode(); 
            MoviesDTO movies = JsonConvert.DeserializeObject<MoviesDTO>(await response.Content.ReadAsStringAsync()); 
            List<MovieItem> itemList = movies.Movies;
            return itemList;
        }
    }
}
