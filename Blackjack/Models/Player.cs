using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public int Balance { get; set; }
        public int Bet { get; set; }
        public Hand Hand { get; set; }

        public Player()
        {
            this.Hand = new Hand();
            this.Balance = 500;
        }

        public void PlaceBet(int betAmount)
        {
            this.Bet = betAmount;
            this.Balance -= betAmount;
        }
    }
}
