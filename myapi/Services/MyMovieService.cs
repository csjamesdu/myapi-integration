using Microsoft.Extensions.Logging;
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
//        private readonly IMyInMemDBService _myInMemDBService;

        public MyMovieService(IHttpClientFactory httpClientFactory, 
            ILogger<MyMovieService> logger) { 
            _httpClientFactory = httpClientFactory; 
             }
        public IEnumerable<MovieItem> getData(bool refresh)
        {
            //var result = _myInMemDBService.GetMovieItemsFromDB(); 
            //if (result == null || !result.Any()) 
            //{   
            //    result = SyncGetMovieItemsByAPI(); 
            //    _myInMemDBService.SaveMovieItemsToDB(result); 
            //    _logger.LogInformation("Result Saved."); }
            //_logger.LogInformation(JsonConvert.SerializeObject(result.ToList()));
            //return result;
            return SyncGetMovieItemsByAPI();
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
            List<MovieItem> FWMovies = await getMoviesFromProvider(FILM_WORLD_API); List<MovieItem> CWMovies = await getMoviesFromProvider(CINEMA_WORLD_API);
            var rawResults = await Task.WhenAll(getMoviesFromProvider(FILM_WORLD_API), getMoviesFromProvider(CINEMA_WORLD_API)); var intermediateResult = rawResults.SelectMany(result => result); foreach (MovieItem item in intermediateResult) { item.ID = item.ID.Substring(2); item.Poster = item.Poster.Replace("http", "https"); }
            IEnumerable<MovieItem> finalResult = intermediateResult.ToList().GroupBy(item => item.ID).Select(group => group.First());
            return finalResult;
        }


        private async Task<List<MovieItem>> getMoviesFromProvider(string provider)
        {
            var httpClient = _httpClientFactory.CreateClient("MyMovieClient"); 
            var request = new HttpRequestMessage(HttpMethod.Get, provider);
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead); 
            response.EnsureSuccessStatusCode(); MoviesDTO movies = JsonConvert.DeserializeObject<MoviesDTO>(await response.Content.ReadAsStringAsync()); 
            List<MovieItem> itemList = movies.Movies;
            return itemList;
        }
    }
}
