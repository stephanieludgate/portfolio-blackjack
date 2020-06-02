using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Card
    {
        public CardValue CardValue { get; set; }
        public CardSuit CardSuit { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            //return ((int)this.CardValue).ToString() + " " + this.CardSuit;
            return this.CardValue + "-" + this.CardSuit.ToString().ElementAt(0)+".png";
        }
    }
    public enum CardValue
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10
    }

    public enum CardSuit
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds
    }
}
