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
            int aceCount = 0;

            // first add all non-aces
            foreach(Card c in Cards)
            {
                if (c.CardValue != CardValue.Ace)
                    sum += (int)c.CardValue;
                else
                    aceCount++;
            }

            // do I have any aces?
            if(aceCount == 1)
            {
                if (sum <= 10)
                    sum += 11;
                else sum += 1;
            } else if(aceCount > 1){
                if (sum + (aceCount - 1) <= 11)
                    sum += 11 + (aceCount - 1);
                else
                    sum += aceCount;
            }

            return sum;
        }

        //public int DealerFullHand()
        //{
        //    int sum = 0;
        //    foreach (Card c in Cards)
        //    {
        //        sum += (int)c.CardValue;
        //    }
        //    return sum;
        //}

        public int DealerConcealedHand()
        {
            int sum = 0;

            foreach (Card c in Cards)
            {
                if (c != Cards.ElementAt(0))
                {
                    if (c.CardValue != CardValue.Ace)
                        sum += (int)c.CardValue;
                    else
                        sum += 11;
                }
            }
            return sum;
        }
    }
}
