using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteCustomTypes.Roulette
{
    public class Bet
    {
        public string Id { get; set; }
        public string IdRoulette { get; set; }
        public string UserId { get; set; }
        public string BetInformation { get; set; }
        public int Amount { get; set; }
        public string DateTimeString { get; set; }

    }
}
