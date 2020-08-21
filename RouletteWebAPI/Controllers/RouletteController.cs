using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RouletteWebAPI.Logic;
using RouletteWebAPI.Model;
using RouletteWebAPI.Transversal;
using StackExchange.Redis;

namespace RouletteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IDatabase Database;
        private IConfiguration Configuration { get; }
        public RouletteController(IDatabase database, IConfiguration configuration)
        {
            Database = database;
            Configuration = configuration;
        }
        [HttpGet]
        public string Get()
        {
            return "Roulette Web Api - desarrollado por: Jhohan Eduardo Capador Diaz";
        }

        [HttpPost]
        [Route("CreateRoulette/")]
        public ResultActionGeneric<Roulette> CreateRoulette()
        {
            return new RouletteLogic(Database, Configuration).CreateRoulette();
        }

        [HttpPut]
        [Route("StartRoulette/")]
        public ResultActionGeneric<Roulette> StartRoulette([FromQuery] string idRoulette)
        {
            return new RouletteLogic(Database, Configuration).StartRoulette(idRoulette);
        }

        [HttpPost]
        [Route("CreateBet/{userId}")]
        public ResultActionGeneric<Bet> CreateBet(string userId, [FromBody] Bet bet)
        {
            return new RouletteLogic(Database, Configuration).CreateBet(userId, bet);
        }

        [HttpGet]
        [Route("CloseBet/")]
        public ResultActionGeneric<List<Bet>> CloseBet([FromQuery] string idRoulette)
        {
            return new RouletteLogic(Database, Configuration).CloseBet(idRoulette);
        }
        [HttpGet]
        [Route("ListRouletteCreate/")]
        public ResultActionGeneric<List<Roulette>> ListRouletteCreate()
        {
            return new RouletteLogic(Database, Configuration).ListRouletteCreate();
        }
    }
}
