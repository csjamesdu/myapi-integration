using myapi.Models;
using System.Threading.Tasks;

namespace myapi.Services
{
    public interface IDataAggregationService
    {
        Task<string> GetMovieList();

        Task<string> GetMovieDetail(string id);

        Task<MovieDetail> GetBestPriceForMovie(string id);
    }
}
