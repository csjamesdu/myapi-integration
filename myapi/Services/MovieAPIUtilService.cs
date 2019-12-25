using Microsoft.Extensions.Logging;
using myapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Services
{
    public class MovieAPIUtilService : IMovieAPIUtilService
    {
        private readonly IAppHttpService _appHttpService;
        private readonly ILogger<MovieAPIUtilService> _logger;

        public MovieAPIUtilService(IAppHttpService appHttpService, ILogger<MovieAPIUtilService> logger)
        {
            _appHttpService = appHttpService;
            _logger = logger;
        }
        public async Task<IEnumerable<MovieItem>> GetMovieListFromAPI(string api, string client)
        {
            try
            {
                var apiResult = await _appHttpService.GetWithClient<MoviesDTO>(api, client);
                return apiResult.Movies;
            }
            catch (Exception e)
            {
                _logger.LogError(api + "List API Fails: " + e);
                return new List<MovieItem>();
            }
        }

        public IEnumerable<MovieItem> MergeAndProcessResults(IEnumerable<MovieItem> original, IEnumerable<MovieItem> others)
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



        public async Task<MovieDetail> GetMovieDetailFromAPI(string api, string client)
        {
            try
            {
                var result = await _appHttpService.GetWithClient<MovieDetail>(api, client);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(api + " Detail API Fails: " + e);
                return null;
            }
        }

        public MovieDetail FindTheBestPrice(List<MovieDetail> detailList)
        {
            if(detailList.Any())
            {
                var minPrice = detailList.Min(e => e.Price);
                var resultObj = detailList.First(e => e.Price == minPrice);

                return resultObj;
            }
            else
            {
                return null;
            }
        }
    }
}
