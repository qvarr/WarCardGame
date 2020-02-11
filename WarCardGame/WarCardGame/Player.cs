using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarCardGame
{
    public abstract class Player
    {
        /// <summary>
        /// Gets or sets all the cards the user played during a round of War.
        /// </summary>
        public List<PlayingCard> CardsPlayedThisRound { get; set; } = new List<PlayingCard>() { };

        /// <summary>
        /// Gets or sets the deck of the player.
        /// </summary>
        public List<PlayingCard> Deck { get; set; } = new List<PlayingCard>() { };

        /// <summary>
        /// Gets the deck size of the player.
        /// </summary>
        public int DeckSize
        {
            get { return Deck.Count(); }
        }

        /// <summary>
        /// Gets or sets a value whether or not the player is the winner of the game.
        /// </summary>
        public bool IsWinner { get; set; } = false;

        /// <summary>
        /// Gets the value whether or not the player object is the user.
        /// </summary>
        public virtual bool IsUserPlayer { get; }

        /// <summary>
        /// Gets the player's card that is being played for the round. The card would be the first card from the player's deck.
        /// </summary>
        public PlayingCard PlayingCard
        {
            get { return Deck.First(); }
        }

        /// <summary>
        /// Gets or sets a value whether or not the player is the winner of a round.
        /// </summary>
        public bool WonRound { get; set; } = false;

        /// <summary>
        ///  Gets or sets a value whether or not the player is the winner of a war.
        /// </summary>
        public bool WonWar { get; set; } = false;

        /// <summary>
        /// Method that displays the player's results of the game. 
        /// </summary>
        public abstract void DisplayPlayerResults();
    }

    public class ComputerPlayer : Player
    {
        /// <summary>
        /// Returns false since this is the ComputerPlayer object.
        /// </summary>
        public override bool IsUserPlayer { get { return false; } }

        /// <summary>
        /// Displays the results of the game if the computer player won.
        /// </summary>
        public override void DisplayPlayerResults()
        {
            if (IsWinner)
            {
                // user lost 
                Console.WriteLine("The computer won the game."); ;
                Console.WriteLine("You lost... better luck next time!");
            }
        }
    }

    public class UserPlayer : Player
    {
        /// <summary>
        /// Returns true since this is the UserPlayer object.
        /// </summary>
        public override bool IsUserPlayer { get { return true; } }

        /// <summary>
        /// Displays the results of the game if the user player won.
        /// </summary>
        public override void DisplayPlayerResults()
        {
            if (IsWinner)
            {
                // user won
                Console.WriteLine("You won! Congrats, you beat the computer!");
            }
        }
    }
}
