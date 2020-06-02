using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Deck
    {
        public ICollection<Card> Cards { get; set; }

        public Deck()
        {
            this.Cards = new List<Card>();
            NewDeck();
            ShuffleDeck();
        }

        public void NewDeck()
        {
            Cards.Clear();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    this.Cards.Add(new Card() { CardValue = value, CardSuit = suit, ImagePath = (value + "-" + suit.ToString().ElementAt(0) + ".png") });

                }
            }
        }

        public void ShuffleDeck()
        {
            Random r = new Random(); 
            Cards = Cards.OrderBy(x => r.Next()).ToList();
        }

        public Card DealCard()
        {
            var myStack = new Stack<Card>(Cards);
            Card topCard = myStack.Pop();
            Cards.Remove(topCard); // remove from list object
            return topCard;
        }
    }
}
