using System;
using System.Collections.Generic;
using System.Text;

namespace WarCardGame
{
    public class PlayingCard
    {
        /// <summary>
        /// Gets or sets the numeric value of the card - the actual property of the card that is compared during gameplay
        /// </summary>
        public int CardValue { get; set; }

        /// <summary>
        /// Gets or sets the card face (ie, is a 2, Jack, Ace, etc.)
        /// </summary>
        public string CardFace { get; set; }

        /// <summary>
        /// Gets or sets the card suit (ie, Hearts, Diamonds, etc.)
        /// </summary>
        public string CardSuit { get; set; }
    }
}
