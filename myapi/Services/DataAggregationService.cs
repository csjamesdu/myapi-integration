using Microsoft.Extensions.Logging;
using myapi.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class DataAggregationService : IDataAggregationService
    {
        private readonly ICinemaWorldAPIService _cinemaWorldAPIService;
        private readonly IFilmWorldAPIService _filmWorldAPIService;
        private readonly IMovieAPIUtilService _movieAPIUtilService;
        private readonly IMemCacheService _memCacheService;
        private readonly ILogger<DataAggregationService> _logger;

        public DataAggregationService(ICinemaWorldAPIService cinemaWorldAPIService,
            IFilmWorldAPIService filmWorldAPIService,
            IMovieAPIUtilService movieAPIUtilService,
            IMemCacheService memCacheService,
            ILogger<DataAggregationService> logger)
        {
            _cinemaWorldAPIService = cinemaWorldAPIService;
            _filmWorldAPIService = filmWorldAPIService;
            _movieAPIUtilService = movieAPIUtilService;
            _memCacheService = memCacheService;
            _logger = logger;
        }
        public async Task<string> GetMovieList()
        {
            var cacheKey = "MovieList";
            string jsonStr;
            if (_memCacheService.CacheHit(cacheKey))
            {
                _logger.LogInformation("*******************cache hit list from service;");
                string resultStr = _memCacheService.GetValue(cacheKey);
                return resultStr;
            }
            else
            {
                var resultFW = await _cinemaWorldAPIService.AsycGetMovieList();
                var resultCW = await _filmWorldAPIService.AsycGetMovieList();

                var resultCollection = _movieAPIUtilService.MergeAndProcessResults(resultFW, resultCW);
                if (resultCollection.Any())
                {
                    MoviesDTO resultObj = new MoviesDTO
                    {
                        Movies = resultCollection.ToList()
                    };
                    jsonStr = JsonConvert.SerializeObject(resultObj);
                    _memCacheService.SetValueWithExpire(jsonStr, cacheKey, TimeSpan.FromMinutes(30));
                    return jsonStr;
                }
                else
                {
                    return null;
                }

            }

        }

        public async Task<string> GetMovieDetail(string id)
        {
            var cacheKey = "MovieDetail" + id;
            if (_memCacheService.CacheHit(cacheKey))
            {
                _logger.LogInformation("*******************cache hit detail from service;");
                return _memCacheService.GetValue(cacheKey);
            }
            else
            {
                var resultObj = await GetMovieDetailFromAPI(id);
                if (resultObj != null)
                {
                    var resultJson = JsonConvert.SerializeObject(resultObj);
                    _memCacheService.SetValueWithExpire(resultJson, cacheKey, TimeSpan.FromMinutes(30));
                    return resultJson;
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<MovieDetail> GetMovieDetailFromAPI(string id)
        {
            var taskFW = _cinemaWorldAPIService.AsyncGetMovieDetail(id);
            var taskCW = _filmWorldAPIService.AsyncGetMovieDetail(id);

            var firstFinished = await Task.WhenAny(taskFW, taskCW);
            var result = await firstFinished;
            result.Poster = PosterResources.PosterDic[id];
            return result;
        }

        public async Task<MovieDetail> GetBestPriceForMovie(string id)
        {
            var resultCW =await _cinemaWorldAPIService.AsyncGetMovieDetail(id);
            var resultFW =await _filmWorldAPIService.AsyncGetMovieDetail(id);
            if (resultCW != null)
            {
                if (resultFW != null)
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
    }
}
