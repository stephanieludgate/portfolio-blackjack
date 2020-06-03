using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Game
    {
        public Guid GameID { get; set; }
        public Player Player { get; set; }
        public Dealer Dealer { get; set; }
        public Deck Deck { get; set; }
        public bool IsActive { get; set; }

        public Game(Guid newID)
        {
            this.GameID = newID;
            this.Player = new Player() { Username = "stephludgate" };
            this.Dealer = new Dealer();
            this.Deck = new Deck();
            this.IsActive = true;
        }

        public bool Stand()
        {
            // compare hands
            return (Player.Hand.SumOfHand() > Dealer.Hand.SumOfHand()); // return true if player has higher value
        }

        public bool Hit()
        {
            Player.Hand.Cards.Add(Deck.DealCard());
            // check to see value of player hand
            if (Player.Hand.SumOfHand() >= 21)
                return false; // game is over
            else
                return true; // player's hand is less than 21, can continue
        }


    }
}
