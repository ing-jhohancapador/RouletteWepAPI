using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteWebAPI.Model
{
    public class Bet
    {
        public string Id { get; set; }
        public string IdRoulette { get; set; }
        public string UserId { get; set; }
        public string Result { get; set; }
        public string Color { get; set; }
        public int Number { get; set; }
        public int Amount { get; set; }
        public string DateTimeString { get; set; }
    }
}
