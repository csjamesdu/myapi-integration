﻿using myapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Services
{
    public interface IMyMovieService
    {
        Task<IEnumerable<MovieItem>> GetMovies();
    }
}
