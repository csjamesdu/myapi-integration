using myapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.DAO
{
    public class MovieItemDAO : IMovieItemDAO
    {
        private readonly MovieItemContext _DbContext;

        public MovieItemDAO(MovieItemContext DbContext)
        {
            _DbContext = DbContext;
        }
        public IEnumerable<MovieItem> GetMovieItemsFromInMemDB()
        {
            IEnumerable<MovieItem> movies = _DbContext.MovieItems;
            return movies;
        }

        public void SaveMovieItemsToInMemDB(IEnumerable<MovieItem> movies)
        {
            _DbContext.MovieItems.AddRange(movies);
            _DbContext.SaveChanges();
        }
    }
}
