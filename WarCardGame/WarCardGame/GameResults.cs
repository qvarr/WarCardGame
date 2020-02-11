using System;
using System.Collections.Generic;
using System.Text;

namespace WarCardGame
{
    public class GameResults
    {
        /// <summary>
        /// Gets or sets the number of rounds played.
        /// </summary>
        public int NumberOfRounds { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of rounds the user won.
        /// </summary>
        public int NumberOfUserRoundWins { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of wars during the game.
        /// </summary>
        public int NumberOfWars { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of ties of war.
        /// </summary>
        public int NumberOfWarTies { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of wars the user won.
        /// </summary>
        public int NumberOfUserWarWins { get; set; } = 0;
    }
}
