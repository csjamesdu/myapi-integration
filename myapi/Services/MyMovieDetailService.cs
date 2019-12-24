using Microsoft.Extensions.Logging;
using myapi.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class MyMovieDetailService : IMyMovieDetailService
    {
        const string FILM_WORLD_API = "api/filmworld/movie/fw";
        const string CINEMA_WORLD_API = "api/cinemaworld/movie/cw";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MyMovieDetailService> _logger;

        public MyMovieDetailService(IHttpClientFactory httpClientFactory,
           ILogger<MyMovieDetailService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<MovieDetail> GetDetailById(string id)
        {
            MovieDetail result = await GetAndCompareLowerPrice(id);
            if(result != null )
            {
                result.Poster = PosterResources.PosterDic[id];
            }         
            return result;
        }

        private async Task<MovieDetail> GetAndCompareLowerPrice(string id)
        {
            var resultCW = await GetMovieDetailFromProvider(CINEMA_WORLD_API, id);
            var resultFW = await GetMovieDetailFromProvider(FILM_WORLD_API, id);

            if(resultCW != null)
            {
                if(resultFW != null)
                {
                    return resultCW.Price < resultFW.Price ? resultCW : resultFW;
                }
                else
                {
                    return resultCW;
                }
            }
            else
            {
                if (resultFW != null)
                {
                    return resultFW;
                }
                else return null;
            }

        }

        private async Task<MovieDetail> GetMovieDetailFromProvider(string provider, string id)
        {
            try
            {
                var requestUri = provider + id;
                var httpClient = _httpClientFactory.CreateClient("MyMovieClient");
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var result = JsonConvert.DeserializeObject<MovieDetail>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch(Exception e)
            {
                _logger.LogError(provider + " Detail API Fails: " + e);
                return null;
            }
        }

    }
}
