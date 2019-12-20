using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    public class MovieItemContext: DbContext
    {
        public MovieItemContext(DbContextOptions<MovieItemContext> options) : base(options)
        {
        }

        public DbSet<MovieItem> MovieItems { get; set; }

    }
}
