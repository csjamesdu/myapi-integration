using myapi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myapi.Services
{
    public interface IMovieAPIUtilService
    {

        Task<IEnumerable<MovieItem>> GetMovieListFromAPI(string api, string client);

        Task<MovieDetail> GetMovieDetailFromAPI(string api, string client);

        IEnumerable<MovieItem> MergeAndProcessResults(IEnumerable<MovieItem> original, IEnumerable<MovieItem> others);

        MovieDetail FindTheBestPrice(List<MovieDetail> detailList);
    }
}
