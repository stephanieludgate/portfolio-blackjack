using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Hand
    {
        public ICollection<Card> Cards { get; set; }

        public Hand()
        {
            this.Cards = new List<Card>();
        }

        public int SumOfHand()
        {
            int sum = 0;
            foreach(Card c in Cards)
            {
                sum += (int)c.CardValue;
            }
            return sum;
        }
    }
}
