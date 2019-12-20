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
    public class MyMovieDetailService : IMyMovieDetailService
    {
        const string FILM_WORLD_API = "api/filmworld/movie/";
        const string CINEMA_WORLD_API = "api/cinemaworld/movie/";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MyMovieDetailService> _logger;

        public MyMovieDetailService(IHttpClientFactory httpClientFactory,
           ILogger<MyMovieDetailService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public MovieDetail GetDetailById(int id)
        {
            MovieDetail result = Task.Run(() => GetMovieDetailFromProvider(CINEMA_WORLD_API, id)).GetAwaiter().GetResult(); ;
            return result;
        }

        private async Task<MovieDetail> GetMovieDetailFromProvider(string provider, int id)
        {
            var requestUri = provider + "cw" + id;
            var httpClient = _httpClientFactory.CreateClient("MyMovieClient");
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            MovieDetail result = JsonConvert.DeserializeObject<MovieDetail>(await response.Content.ReadAsStringAsync());
            return result;
        }

    }
}
