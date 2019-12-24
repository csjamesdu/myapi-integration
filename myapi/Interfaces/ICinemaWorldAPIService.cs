using myapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myapi.Services
{
    public interface ICinemaWorldAPIService
    {
        Task<IEnumerable<MovieItem>> AsycGetMovieList();
        Task<MovieDetail> AsyncGetMovieDetail(string id);
    }
}
