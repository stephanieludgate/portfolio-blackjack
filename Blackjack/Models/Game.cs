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
        public int Bet { get; set; }
        public GameState GameState { get; set; }
        public bool DealerMove { get; set; }

        public Game(Guid newID, String username)
        {
            this.GameID = newID;
            this.Player = new Player() { Username = username };
            this.Dealer = new Dealer();
            //this.Deck = new Deck();
            //this.GameState = GameState.PLAY;
            //this.DealerMove = false;
        }

        public void NewRound()
        {
            // CLEAR any existing cards/hands
            //this.Deck.Cards.Clear();
            //this.Player.Hand.Cards.Clear();
            //this.Dealer.Hand.Cards.Clear();

            // NEW round
            this.Deck = new Deck();
            this.GameState = GameState.PLAY;
            this.DealerMove = false;

            // CHECK for existing cards
            if (this.Player.Hand.Cards.Count > 0)
            {
                this.Player.Hand.Cards.Clear();
                this.Dealer.Hand.Cards.Clear();
            }
            // Add 2 cards each
            this.Player.Hand.Cards.Add(this.Deck.DealCard());
            this.Dealer.Hand.Cards.Add(this.Deck.DealCard());
            this.Player.Hand.Cards.Add(this.Deck.DealCard());
            this.Dealer.Hand.Cards.Add(this.Deck.DealCard());
        }

        public void PlaceBet(int betting)
        {
            this.Bet = betting;
            this.Player.Balance -= betting;
        }

        public void WinBet()
        {
            this.Player.Balance += (this.Bet * 2);
            this.Bet = 0;
        }

        public void LoseBet()
        {
            this.Bet = 0;
        }

        public void PlayerHit()
        {
            this.Player.Hand.Cards.Add(this.Deck.DealCard());
        }
        public void DealerHit()
        {
            this.Dealer.Hand.Cards.Add(this.Deck.DealCard());
        }

        public void CompareHands()
        {
            if (this.Dealer.Hand.SumOfHand() > 21 || this.Player.Hand.SumOfHand() > this.Dealer.Hand.SumOfHand())
            {
                this.GameState = GameState.WIN;
                this.WinBet();
            }
            else
            {
                this.GameState = GameState.LOSE;
                this.LoseBet();
            }
        }
    }

    public enum GameState
    {
        PLAY,
        WIN,
        LOSE
    }
}
