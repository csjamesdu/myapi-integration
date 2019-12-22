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

        public MovieDetail GetDetailById(string id)
        {
            MovieDetail result = GetAndCompareLowerPrice(id);
            if(result != null )
            {
                result.Poster = PosterResources.PosterDic[id];
            }         
            return result;
        }

        private MovieDetail GetAndCompareLowerPrice(string id)
        {
            var resultCW = SyncGetDetailByIdAndProvider(CINEMA_WORLD_API, id);
            var resultFW = SyncGetDetailByIdAndProvider(FILM_WORLD_API, id);

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

        private MovieDetail SyncGetDetailByIdAndProvider(string provider, string id)
        {
            try
            {
                var result = Task
                    .Run(() => GetMovieDetailFromProvider(provider, id))
                    .GetAwaiter().GetResult();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(provider + " Detail API Fails: " + e);
                return null;
            }
        }

        private IEnumerable<MovieDetail> SyncGetDetailById(string id)
        {
            try
            {
                IEnumerable<MovieDetail> result = Task
                    .Run(() => AsyncGetDetailById(id))
                    .GetAwaiter().GetResult();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("Detail API Fails: " + e);
                return new List<MovieDetail>();
            }
        }

        private async Task<IEnumerable<MovieDetail>> AsyncGetDetailById(string id)
        {
            
            var rawResult = await Task.WhenAll(GetMovieDetailFromProvider(CINEMA_WORLD_API, id),
                GetMovieDetailFromProvider(FILM_WORLD_API, id));
            var minimumPrice = rawResult.Min(item => item.Price);
            var itemWithMiminumPrice = rawResult.Where(item => item.Price == minimumPrice).ToList();
            return itemWithMiminumPrice;
        }

        private async Task<MovieDetail> GetMovieDetailFromProvider(string provider, string id)
        {
            var requestUri = provider + id;
            var httpClient = _httpClientFactory.CreateClient("MyMovieClient");
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var result = JsonConvert.DeserializeObject<MovieDetail>(await response.Content.ReadAsStringAsync());
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                result = null;
//                _logger.LogInformation("***************Not Found Hit!");
            }           
            return result;
        }

    }
}
