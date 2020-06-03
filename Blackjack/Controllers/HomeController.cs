using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blackjack.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Blackjack.Controllers
{
    public class HomeController : Controller
    {
        public static List<Game> gameList = new List<Game>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            ViewData["Message"] = "Specific game instructions here...";

            return View();
        }

        public IActionResult Start()
        {
            // create instance of a game
            Guid newID = Guid.NewGuid();
            Game newGame = new Game(newID);

            // Add 2 cards each to start
            newGame.Player.Hand.Cards.Add(newGame.Deck.DealCard());
            newGame.Dealer.Hand.Cards.Add(newGame.Deck.DealCard());
            newGame.Player.Hand.Cards.Add(newGame.Deck.DealCard());
            newGame.Dealer.Hand.Cards.Add(newGame.Deck.DealCard());

            gameList.Add(newGame);
            HttpContext.Session.SetString("CurrentGameID", newID.ToString());

            return View(newGame);
        }

        [HttpPost]
        public IActionResult Start(string str)
        {
            Guid currentGameID = new Guid(HttpContext.Session.GetString("CurrentGameID"));
            Game currentGame = gameList.Where(g => g.GameID == currentGameID).FirstOrDefault();

            currentGame.Player.Bet = Int32.Parse(HttpContext.Request.Form["betPlaced"]);
            ViewData["Bet"] = currentGame.Player.Bet;

            ViewData["Temp"] = HttpContext.Session.GetString("CurrentGameID");

            //return View(currentGame);
            return RedirectToAction("Play");
        }


        public IActionResult Play()
        {
            Guid currentGameID = new Guid(HttpContext.Session.GetString("CurrentGameID"));
            Game currentGame = gameList.Where(g => g.GameID == currentGameID).FirstOrDefault();
            return View(currentGame);
            //Game newGame = new Game() { GameID = 1, IsActive = true };
            //newGame.Player.Username = "HELP";
            //this.Games.Add(newGame);

            //newGame.Player.Hand.Cards.Add(newGame.Deck.DealCard());
            //newGame.Dealer.Hand.Cards.Add(newGame.Deck.DealCard());
            //newGame.Player.Hand.Cards.Add(newGame.Deck.DealCard());
            //newGame.Dealer.Hand.Cards.Add(newGame.Deck.DealCard());

            //ViewData["Winner"] = newGame.Stand().ToString();

            //return View(newGame);
            //gameCount++;
            //Game newGame = new Game() { GameID = gameCount, IsActive = true };
            //newGame.Player.Username = "PLEASE";
            //this.Games.Add(newGame);
            //return View(newGame);

            //gameCount++;
            //this.Game = new Game() { GameID = gameCount, IsActive = true };
            //this.Game.Player.Username = "PLEASE";
            //return View(this.Game);
        }

        public IActionResult Hit()
        {
            Guid currentGameID = new Guid(HttpContext.Session.GetString("CurrentGameID"));
            Game currentGame = gameList.Where(g => g.GameID == currentGameID).FirstOrDefault();

            currentGame.Player.Hand.Cards.Add(currentGame.Deck.DealCard());
            currentGame.Dealer.Hand.Cards.Add(currentGame.Deck.DealCard());

            return RedirectToAction("Play");


        }

        //[HttpPost]
        //public async Task<IActionResult> Play([Bind("GameID, Player, Dealer, Deck")]Game game)
        //{
        //    keeptrack++;
        //    if(keeptrack > 1)
        //    {

        //    }
        //    else
        //    {
        //        game.Player.Hand.Cards.Add(game.Deck.DealCard());
        //        game.Dealer.Hand.Cards.Add(game.Deck.DealCard());
        //        game.Player.Hand.Cards.Add(game.Deck.DealCard());
        //        game.Dealer.Hand.Cards.Add(game.Deck.DealCard());
        //    }

        //    ViewData["Winner"] = "BET " + game.Player.Bet.ToString() + " GAME ID " + game.GameID;

        //    return View(game);
        //}

        //[HttpPost]
        //public async Task<IActionResult> MakeMove([Bind("GameID, Player, Dealer, Deck")]Game game, SelectListItem item)
        //{
        //    //Game currentGame = Games.Where(g => g.GameID == game.GameID) as Game;
        //    //game.Player.Hand.Cards.Add(game.Deck.DealCard());
        //    //game.Dealer.Hand.Cards.Add(game.Deck.DealCard());
        //    //game.Player.Hand.Cards.Add(game.Deck.DealCard());
        //    //game.Dealer.Hand.Cards.Add(game.Deck.DealCard());

        //    ViewData["Winner"] = "BET " + game.Player.Bet.ToString() + " GAME ID " + game.GameID + " Move " + item.Value;
        //    //ViewData["Winner"] = game.Stand().ToString();

        //    return View("Play", this.Game);
        //}

        public void Stand()
        {
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
