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
            ClearSessionData();
            return View();
        }

        [HttpPost]
        public IActionResult Index(String str)
        {
            // retrieve & save username
            String newUser = "player";
            if(!String.IsNullOrWhiteSpace(HttpContext.Request.Form["username"]))
                newUser = HttpContext.Request.Form["username"];
            HttpContext.Session.SetString("CurrentUser", newUser);

            return RedirectToAction("Start");
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public IActionResult Start()
        {
            // check is game is currently active
            if(HttpContext.Session.GetString("CurrentGameID") != null)
            {
                Game currentGame = GetGameById();
                // Current game in session, check for balance greater than 0
                if(currentGame.Player.Balance > 0)
                {
                    if (currentGame.GameState == GameState.PLAY) // shouldn't be allowed here...
                        return RedirectToAction("Play");
                    else
                    {
                        currentGame.NewRound();
                        return View(currentGame);
                    }
                }
                else
                {
                    // Player is out of money.... clear session data & redirect to game over page (w/button to start new user)
                    ClearSessionData();
                    return RedirectToAction("GameOver");
                }
            }
            else
            {
                // create instance of a game
                Guid newID = Guid.NewGuid();
                String currentUser = HttpContext.Session.GetString("CurrentUser");
                Game newGame = new Game(newID, currentUser);
                newGame.NewRound();

                gameList.Add(newGame);
                HttpContext.Session.SetString("CurrentGameID", newID.ToString());

                return View(newGame);
            }    
        }

        [HttpPost]
        public IActionResult Start(string str)
        {
            Game currentGame = GetGameById();

            // make sure bet is valid
            if(Int32.Parse(HttpContext.Request.Form["betPlaced"]) >= 1 && Int32.Parse(HttpContext.Request.Form["betPlaced"]) <=100)
            {
                currentGame.PlaceBet(Int32.Parse(HttpContext.Request.Form["betPlaced"]));
                return RedirectToAction("Play");
            }
            else // INVALID bet
            {
                return View(currentGame);
            }
               
        }


        public IActionResult Play()
        {
            Game currentGame = GetGameById(); 

            // whose move is it
            if(!currentGame.DealerMove) // PLAYERS MOVE
            {
                if (currentGame.Player.Hand.SumOfHand() == 21) // PLAYER WON
                {
                    currentGame.GameState = GameState.WIN;
                    currentGame.WinBet();
                } 
                else if(currentGame.Player.Hand.SumOfHand() > 21) // PLAYER LOST
                {
                    currentGame.GameState = GameState.LOSE;
                    currentGame.LoseBet();
                }
                // IMPLIED if player's move and less than 21, do nothing
            }
            else // DEALERS MOVE
            {
                if(currentGame.Dealer.Hand.SumOfHand() < 17)
                {
                    // dealer must hit
                    currentGame.DealerHit();
                }
                // regardless of dealer hit/stand, compare hands
                currentGame.CompareHands();
            }

            return View(currentGame);
        }

        public IActionResult Hit()
        {
            Game currentGame = GetGameById();
            currentGame.PlayerHit();
            return RedirectToAction("Play");
        }

        public IActionResult Stand()
        {
            Game currentGame = GetGameById();
            currentGame.DealerMove = true;
            return RedirectToAction("Play");
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

        public IActionResult GameOver()
        {
            return View();
        }

        public void ClearSessionData()
        {
            HttpContext.Session.Clear();
        }

        public Game GetGameById()
        {
            Guid currentGameID = new Guid(HttpContext.Session.GetString("CurrentGameID"));
            return gameList.Where(g => g.GameID == currentGameID).FirstOrDefault();
        }
    }
}
