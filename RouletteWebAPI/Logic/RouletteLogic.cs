using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RouletteWebAPI.Model;
using RouletteWebAPI.Transversal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteWebAPI.Logic
{
    public class RouletteLogic
    {
        private const string KeyPositionRoulette = "RLTPosition";
        private const string CodeRoulette = "RTL";
        private const string KeyPositionBet = "BTPosition";
        private const string CodeBet = "BT";
        private readonly IDatabase Database;
        private IConfiguration Configuration { get; }
        public RouletteLogic(IDatabase database, IConfiguration configuration)
        {
            Database = database;
            Configuration = configuration;
        }
        public ResultActionGeneric<Roulette> CreateRoulette()
        {
            try
            {
                string stateRoulette = "Cerrada";
                string lastPosition = string.Empty;
                int newPosition = 0;
                if (!Database.KeyExists(KeyPositionRoulette))
                    newPosition = 1;
                else
                {
                    lastPosition = Database.StringGet(KeyPositionRoulette);
                    newPosition = Int16.Parse(lastPosition) + 1;
                }
                Database.StringGetSet(KeyPositionRoulette, newPosition);
                string idRoulette = $"{CodeRoulette}{newPosition}";
                Database.StringSet(idRoulette, stateRoulette);
                return new ResultActionGeneric<Roulette> { Result = new Roulette { Id = idRoulette, State = stateRoulette } };
            }
            catch (Exception ex)
            {
                return new ResultActionGeneric<Roulette> { Error = true, Message = ex.ToString() };
            }
        }

        public ResultActionGeneric<Roulette> StartRoulette(string idRoulette)
        {
            try
            {
                string stateRoulette = "Cerrada";
                if (Database.KeyExists(idRoulette))
                {
                    Database.StringGetSet(idRoulette, "Abierta");
                    stateRoulette = "Abierta";
                }

                return new ResultActionGeneric<Roulette> { Result = new Roulette { Id = idRoulette, State = stateRoulette } };
            }
            catch (Exception ex)
            {
                return new ResultActionGeneric<Roulette> { Error = true, Message = ex.ToString() };
            }
        }

        public ResultActionGeneric<Bet> CreateBet(string userId, Bet bet)
        {
            try
            {
                Random randomLuck = new Random();
                string lastPosition = string.Empty;
                int newPosition = 0;
                bet.UserId = userId;
                if (IsValidateBet(bet))
                    bet.Result = randomLuck.Next(1, 3) == 1 ? "Gano" : "Perdio";
                else
                    bet.Result = "Error, la apuesta no es valida.";

                if (!Database.KeyExists(KeyPositionBet))
                    newPosition = 1;
                else
                {
                    lastPosition = Database.StringGet(KeyPositionBet);
                    newPosition = Int16.Parse(lastPosition) + 1;
                }
                Database.StringGetSet(KeyPositionBet, newPosition);
                bet.Id = $"{CodeBet}{newPosition}";
                bet.DateTimeString = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                Database.StringSet(bet.Id, JsonConvert.SerializeObject(bet));

                return new ResultActionGeneric<Bet> { Result = bet };
            }
            catch (Exception ex)
            {
                return new ResultActionGeneric<Bet> { Error = true, Message = ex.ToString() };
            }
        }

        private bool IsValidateBet(Bet bet)
        {
            int maxAmount = Configuration.GetValue<int>("MaxAmount");
            string color1 = Configuration.GetValue<string>("Color1");
            string color2 = Configuration.GetValue<string>("Color2");
            int minNumber = Configuration.GetValue<int>("MinNum");
            int maxNumber = Configuration.GetValue<int>("MaxNum");

            if (Database.KeyExists(bet.IdRoulette))
                if (Database.StringGet(bet.IdRoulette) != "Abierta")
                    return false;



            if (bet.Amount < maxAmount && bet.Amount > 0)
                return false;

            if (bet.Color.Trim().ToUpper() != color1.ToUpper() && bet.Color.Trim().ToUpper() != color2.ToUpper())
                return false;

            if (bet.Number < minNumber || bet.Number > maxNumber)
                return false;

            return true;
        }
        public ResultActionGeneric<List<Bet>> CloseBet(string idRoulette)
        {
            try
            {
                List<Bet> listBets = new List<Bet>();
                int betPosition = Int16.Parse(Database.StringGet(KeyPositionBet));
                if (Database.KeyExists(idRoulette))
                {
                    for (int i = 1; i <= betPosition; i++)
                    {
                        Bet bet = JsonConvert.DeserializeObject<Bet>(Database.StringGet($"{CodeBet}{i}"));
                        if (bet.Result != "Error, la apuesta no es valida." && idRoulette == bet.IdRoulette)
                            listBets.Add(bet);

                    }
                    Database.StringGetSet(idRoulette, "Cerrada");
                }

                return new ResultActionGeneric<List<Bet>> { Result = listBets };
            }
            catch (Exception ex)
            {
                return new ResultActionGeneric<List<Bet>> { Error = true, Message = ex.ToString() }; ;
            }
        }

        public ResultActionGeneric<List<Roulette>> ListRouletteCreate()
        {
            try
            {
                List<Roulette> listRoulette = new List<Roulette>();
                int roulettePosition = Int16.Parse(Database.StringGet(KeyPositionRoulette));
                for (int i = 1; i <= roulettePosition; i++)
                {
                    Roulette roulette = new Roulette();
                    roulette.Id = $"{CodeRoulette}{i}";
                    roulette.State = Database.StringGet(roulette.Id);
                    listRoulette.Add(roulette);
                }
                return new ResultActionGeneric<List<Roulette>> { Result = listRoulette };
            }
            catch (Exception ex)
            {
                return new ResultActionGeneric<List<Roulette>> { Error = true, Message = ex.ToString() };
            }
        }
    }
}
