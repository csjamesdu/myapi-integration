﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapi.Models;
using myapi.Services;
using Newtonsoft.Json;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {

        private readonly IMyMovieDetailService _myMovieDetailService;

        public PriceController(IMyMovieDetailService myMovieDetailService)
        {
            _myMovieDetailService = myMovieDetailService;
        }

        // GET: api/Price/5
        [HttpGet("{id}", Name = "GetPrice")]
        public async Task<ActionResult<string>> Get(string id)
        {
            MovieDetail detail = await _myMovieDetailService.GetDetailById(id);
            var JsonStr = JsonConvert.SerializeObject(detail);
            return Ok(JsonStr);
        }

    }
}
