using myapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.DAO
{
    public interface IMovieItemDAO
    {
        public IEnumerable<MovieItem> GetMovieItemsFromInMemDB();

        public void SaveMovieItemsToInMemDB(IEnumerable<MovieItem> movies);
    }
}
