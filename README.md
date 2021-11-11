# Blackjack
The goal of this project was to develop a playable game of blackjack, with some basic rules.  This project uses ASP.NET Core MVC, no database was required for this.  Players are first asked to enter a username.\
<img width="760" alt="game start" src="https://user-images.githubusercontent.com/58275084/141332968-d9bd4a18-ec9f-4766-8822-c9f0a6c2208c.png">

### Place Bet
Once a username is established, players are asked to place a bet (between $1-100) - starting balance is $500.
<img width="760" alt="place bet" src="https://user-images.githubusercontent.com/58275084/141332974-862eda1c-7c6b-4433-bd73-761207ad3d91.png">

### Playing Hand
Once a [valid] bet is placed, the player is dealt two cards.  The dealer is also dealt two cards, though only the top card can be viewed.  Players can choose to either 'Hit' or 'Stand'.  If the player chooses to 'Stand', the result of the game is shown (win/loss).  If the player chooses to 'Hit' AND their accumulated total is still less than 21, they will have the chance to choose again.  If their total is more than 21, the player lost.  At any point, if the player's hand equals exactly 21, they automatically win.  As long as the player has money remaining, they can continue to play.\
<img width="760" alt="hand dealt" src="https://user-images.githubusercontent.com/58275084/141332977-87895f75-d348-4c78-92e9-0aa4815de769.png">
<img width="760" alt="won hand" src="https://user-images.githubusercontent.com/58275084/141332979-61c2c7dc-ab58-4659-b411-d369c896476f.png">

### Game Over
Once the player has exhausted all of their funds, the game is over, and the hosue wins.
<img width="760" alt="game over" src="https://user-images.githubusercontent.com/58275084/141332972-9d58490c-8cea-4a78-8adc-cab3187369e5.png">

** Note this game does not deal with actual money and a player can never have a negative balance **
