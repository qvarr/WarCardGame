using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGame
{
    class Program
    {
        // Initialize the required objects needed to create the 52-deck of cards, PlayingCardSuits and PlayingCardvalues
        // These 2 variables contain the card suits and the card faces in a generic 52-card deck, these variables should never be altered during the run of the program.
        private static readonly List<string> PlayingCardSuits = new List<string>() { "Diamonds", "Hearts", "Clubs", "Spades" };
        private static readonly List<KeyValuePair<string, int>> PlayingCardValues = new List<KeyValuePair<string, int>>()
        {
            { new KeyValuePair<string, int>("2", 2) },
            { new KeyValuePair<string, int>("3", 3) },
            { new KeyValuePair<string, int>("4", 4) },
            { new KeyValuePair<string, int>("5", 5) },
            { new KeyValuePair<string, int>("6", 6) },
            { new KeyValuePair<string, int>("7", 7) },
            { new KeyValuePair<string, int>("8", 8) },
            { new KeyValuePair<string, int>("9", 9) },
            { new KeyValuePair<string, int>("10", 10) },
            { new KeyValuePair<string, int>("J", 11) },
            { new KeyValuePair<string, int>("Q", 12) },
            { new KeyValuePair<string, int>("K", 13) },
            { new KeyValuePair<string, int>("A", 14) }
        };

        /// <summary>
        /// The console program. It will first call IntroduceGame to introduce the user and prompt them whether or not they want to play the game. If yes, the program will proceed as normal, if no, the program will greet the user goodbye and close.
        /// If the user chooses yes, it will enter the main while loop to keep playing the game until the user says they no longer want to play when prompted to replay.
        /// </summary>
        static void Main()
        {
            var playGame = IntroduceGame();

            // will keep running the game until playGame is set to false
            while(playGame)
            {
                var userPlayer = new UserPlayer();
                var computerPlayer = new ComputerPlayer();

                DisplayGameRules();
                SetupGame(userPlayer, computerPlayer);
                var results = PlayGame(userPlayer, computerPlayer);
                DisplayGameResults(userPlayer, computerPlayer, results);

                // ask if the user would like to replay the game
                Console.WriteLine();
                Console.WriteLine("Please press 'Y' if you would like to play again. Otherwise, press any other key to stop and exit the game.");
                Console.WriteLine();

                var response = Console.ReadKey();
                playGame = response.Key.Equals(ConsoleKey.Y);
            }

            // only exit the loop if the user chose to stop playing
            Console.WriteLine();
            Console.WriteLine("Hope to see you again, goodbye!");
            Console.WriteLine("Closing game.");
        }

        /// <summary>
        /// Prompts the user if they would like to play the card game, War. If the user says no, returns false and proceeds to closing the game/program.
        /// If the user says yes, it returns true to continue on with the program.
        /// </summary>
        /// <returns>boolean</returns>
        public static bool IntroduceGame()
        {
            var isPlayGame = false;

            // ask if the user wants to play - mistakes can happen!
            Console.WriteLine("Would you like to play the card game, War? Press 'N' to exit, else press any other key to continue.");
            var response = Console.ReadKey();

            if(response.Key.Equals(ConsoleKey.N))
            {
                return isPlayGame;
            }

            isPlayGame = true;

            // welcome the user, introduce the game/program, and display the rules of the card game, War
            Console.WriteLine("Welcome to the card game, War, adapted to a computer console game.");

            return isPlayGame;
        }

        /// <summary>
        /// Displays the rules of the card game, War.
        /// </summary>
        public static void DisplayGameRules()
        {
            Console.WriteLine();
            Console.WriteLine("-------- RULES OF THE CARD GAME --------");
            Console.WriteLine();
            Console.WriteLine("You, the user, will play against the game's computer player in the card game War. You and the computer will be dealt half of a 52-card deck, giving both you and the computer a deck of 26 cards.");
            Console.WriteLine("Both players will take the first card off of their deck and play it. The player with the higher value card wins that round and takes their card and the loser's card and adds it to the bottom of their deck.");
            Console.WriteLine("If both of the players' cards are of the same value, resulting in a tie, then a war commences.");
            Console.WriteLine("Note: The suit of the card does not affect the value. The following order of value from lowest to highest is:");
            Console.WriteLine();
            Console.WriteLine("     2 < 3 < 4 < 5 < 6 < 7 < 8 < 9 < 10 < J < Q < K < A");
            Console.WriteLine();
            Console.WriteLine("*** When there is a War:");
            Console.WriteLine("A war is when both players reveal a card of the same value.");
            Console.WriteLine("Both players will draw 3 cards and and not reveal their values. (3 cards = 3 letters in the word 'War'. The players will then draw one more card to play during the war.");
            Console.WriteLine("The player with the higher fourth card wins all of the cards played during that round, meaning, the card the loser of the round played that started the war, the unrevealed cards, and the card they played for war.");
            Console.WriteLine("If there is a tie during war, repeat the steps of a war and keep repeating until there is a winner.");
            Console.WriteLine("If a player would have less than 4 cards in their deck in the middle of war, they will draw up to 1 less card of their deck. That last card will be the card they play for the war.");
            Console.WriteLine("If a player only has one card left in their deck during a war, their last card will be the card they play during the war.");
            Console.WriteLine("The game ends when one player gets all of the cards/one player loses all of their cards. The winner of the game is who wins all the cards.");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a message indicating whether or not the user won or lost the card game. This function also displays the overall stats of the
        /// game, specifically: # of rounds, # of wars, # of rounds the user won, and the # of wars the user won
        /// </summary>
        /// <param name="user">The player object of the user.</param>
        /// <param name="comp">The player object of the computer.</param>
        /// <param name="results">Results of the game.</param>
        public static void DisplayGameResults(Player user, Player comp, GameResults results)
        {
            // display either the user or the computer player's results depending if the user won or not
            if (user.IsWinner)
            {
                user.DisplayPlayerResults();
            }
            else
            {
                comp.DisplayPlayerResults();
            }

            // display the overall game results
            Console.WriteLine();
            Console.WriteLine("--------- Overall Game Results of this Playthrough---------");
            Console.WriteLine();
            Console.WriteLine($"Number of Rounds: {results.NumberOfRounds}");
            Console.WriteLine($"Number of Wars: {results.NumberOfWars}");
            Console.WriteLine($"Number of Rounds You Won: {results.NumberOfUserRoundWins}");
            Console.WriteLine($"Number of Wars You Won: {results.NumberOfUserWarWins}");
            Console.WriteLine($"Number of Wars that Tied: {results.NumberOfWarTies}");
        }

        /// <summary>
        /// Creates the deck of cards that will be used for current runtime of the game. Creates the 52-card deck by looping thru
        /// the PlayingCardSuits and the PlayingCardValues private variables.
        /// </summary>
        /// <returns>List of PlayingCard objects</returns>
        public static List<PlayingCard> CreateCardDeck()
        {
            var deckOfCards = new List<PlayingCard>() { };

            // loop thru the suits and the card values
            foreach (var suit in PlayingCardSuits)
            {
                foreach (var cardValue in PlayingCardValues)
                {
                    var playingCard = new PlayingCard()
                    {
                        CardSuit = suit,
                        CardFace = cardValue.Key,
                        CardValue = cardValue.Value
                    };

                    // add this playing card to deckOfCards
                    deckOfCards.Add(playingCard);
                }
            }

            return deckOfCards;
        }

        /// <summary>
        /// Takes the 52-card deck and splits it evenly into 2 decks of 26 cards between the user player and the computer player.
        /// </summary>
        /// <param name="deck">A list containing 52 Playing Card objects that will be split in half evenly between the user and the computer player.</param>
        /// <param name="user">The user's player object.</param>
        /// <param name="comp">The computer's player object.</param>
        public static void DealDeck(List<PlayingCard> deck, UserPlayer user, ComputerPlayer comp)
        {
            // shuffle the cards by OrderBy Random
            var rand = new Random();

            // have 52 cards - shuffle deck and deal the cards out to user and computer players
            deck = deck.OrderBy(x => rand.Next()).ToList();

            // half the deck and assign each player 26 playing cards
            for (var i = 0; i < deck.Count() / 2; i++)
            {
                user.Deck.Add(deck[i]);
            }

            for(var i = deck.Count() / 2; i < deck.Count(); i++)
            {
                comp.Deck.Add(deck[i]);
            }
        }

        /// <summary>
        /// Sets up the card deck for the game by creating the 52-card deck and dealing the cards evenly to the user player and computer player.
        /// </summary>
        /// <param name="user">The user's player object.</param>
        /// <param name="comp">The computer's player object.</param>
        public static void SetupGame(UserPlayer user, ComputerPlayer comp)
        {
            Console.WriteLine();
            Console.WriteLine("... Setting Up Deck ...");
            Console.WriteLine();
            Console.WriteLine("    ... Shuffling Cards ...");
            Console.WriteLine();

            // initialize required objects
            var deck = CreateCardDeck();

            Console.WriteLine("        ... Dealing the Cards ...");
            Console.WriteLine();

            // deal the cards evenly between the players
            DealDeck(deck, user, comp);
        }

        /// <summary>
        /// Setups up the war for the players and runs the war. The main while loop will keep looping until there is no longer a tie in war. This function will remove all of the cards the loser played this round from their deck and add those cards to bottom of the winner's deck.
        /// This function will update the NumberOfWars and NumberofWarTies property in the results object.
        /// </summary>
        /// <param name="user">The user player object.</param>
        /// <param name="comp">The computer player object.</param>
        /// <param name="results">The GameResults object.</param>
        public static void War(UserPlayer user, ComputerPlayer comp, GameResults results)
        {
            // initialize required variables to run an event of war
            user.WonWar = false;
            comp.WonWar = false;
            user.CardsPlayedThisRound = new List<PlayingCard>() { };
            comp.CardsPlayedThisRound = new List<PlayingCard>() { };
            var players = new List<Player>() { user, comp };
            var replayWar = true;

            while(replayWar)
            {
                var userPlayingCard = user.PlayingCard;
                var compPlayingCard = comp.PlayingCard;

                // first need to move each player's current Playing card to property CardsPlayedThisRound
                    // the current playing card was what the players tied on, which led them to war
                user.CardsPlayedThisRound.Add(userPlayingCard);
                comp.CardsPlayedThisRound.Add(compPlayingCard);

                // remove the current PlayingCard from their deck to draw a new PlayingCard for war
                user.Deck.Remove(userPlayingCard);
                comp.Deck.Remove(compPlayingCard);

                // need to set up the players for War - loop thru the 2 players to get their cards used for war
                foreach (var player in players)
                {
                    // need to make sure that players have more than 4 cards in their deck
                    if (player.DeckSize >= 4)
                    {

                        // get the first 3 cards from their deck and move it to CardsPlayedThisRound
                        var warCards = player.Deck.Take(3);
                        player.CardsPlayedThisRound.AddRange(warCards);

                        // remove the warCards from their deck
                        player.Deck = player.Deck.Where(d => !warCards.Contains(d)).ToList();
                    }
                    else
                    {
                        // if player has less than 4 cards, then war cards are 1 less than max deck size
                        // if player only has 1 card in deck, that is their playingCard - don't need to do anything
                        if (player.DeckSize > 1)
                        {
                            var warCards = player.Deck.Take(player.DeckSize - 1);
                            player.CardsPlayedThisRound.AddRange(warCards);

                            // remove the warCards from their deck
                            player.Deck = player.Deck.Where(d => !warCards.Contains(d)).ToList();
                        }
                    }
                }

                // commence War!
                Console.WriteLine("-------------------------");
                Console.WriteLine(" ********* WAR ********* ");
                Console.WriteLine("-------------------------");
                Console.WriteLine();

                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();

                // display the playing card
                Console.WriteLine($" You have played ... the { user.PlayingCard.CardFace } of { user.PlayingCard.CardSuit }.");
                Console.WriteLine($" The computer has played ... the { comp.PlayingCard.CardFace } of { comp.PlayingCard.CardSuit }.");
                Console.WriteLine();

                // need to explicitly check who the winner is because their can be a tie
                user.WonWar = user.PlayingCard.CardValue > comp.PlayingCard.CardValue;
                comp.WonWar = user.PlayingCard.CardValue < comp.PlayingCard.CardValue;

                user.WonRound = user.WonWar;
                comp.WonRound = comp.WonWar;

                // only exit this loop if there is NO tie
                replayWar = user.WonWar.Equals(comp.WonWar);

                // record the number of war ties
                if (replayWar)
                {
                    results.NumberOfWarTies++;
                }

                // record number of wars
                results.NumberOfWars++;
            }

            // determine who is the winner of this war
            var winnerOfWar = players.FirstOrDefault(p => p.WonWar);
            var loserOfWar = players.FirstOrDefault(p => !p.WonWar);

            // take all of the loser's cards played this round and add them to the winner's deck
            // remove the loser's cards played this round from their deck
            var loserPlayingCard = loserOfWar.PlayingCard;
            loserOfWar.CardsPlayedThisRound.Add(loserPlayingCard);
            loserOfWar.Deck.Remove(loserPlayingCard);
            winnerOfWar.Deck.AddRange(loserOfWar.CardsPlayedThisRound);
            winnerOfWar.Deck.AddRange(winnerOfWar.CardsPlayedThisRound);

            // move winner's first card on deck (their playing card) to the bottom of their deck
            var winnerPlayingCard = winnerOfWar.PlayingCard;
            winnerOfWar.Deck.Remove(winnerPlayingCard);
            winnerOfWar.Deck.Add(winnerPlayingCard);

            // display the number of cards the user player lost or won this round of war
            if (winnerOfWar.IsUserPlayer)
            {
                // if the user won, display the list of cards they won from the computer
                Console.WriteLine("You won the war!");
                Console.WriteLine();
                Console.WriteLine("You won the following cards...");
                Console.WriteLine();
            }
            else
            {
                // if the user won, display the list of cards they won from the computer
                Console.WriteLine("You lost the war...");
                Console.WriteLine();
                Console.WriteLine("You lost the following cards...");
                Console.WriteLine();
            }

            // loop thru the cards the user won/lost this round of war
            foreach (var card in loserOfWar.CardsPlayedThisRound)
            {
                Console.WriteLine($"    The {card.CardFace} of {card.CardSuit} card.");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Runs the main part of the card game, war. This function has both players play the first cards of their deck. If there is a tie, PlayGame will call the War() function to handle the war part of the card game.
        /// If there is no tie, the function will compare which card is higher to determine the winner. The loser will then lose their card and the winner will gain back their card and the loser's card and add the cards to the bottom of the deck.
        /// The while loop in this function will keep looping until one of the players no longer has cards in their deck, meaning the while loop will check if player object's DeckSize property is 0.
        /// This function will return a GameResults object containing the stats of the game, such as number of rounds, number of rounds the user won, etc..
        /// </summary>
        /// <param name="user">The user player object.</param>
        /// <param name="comp">The computer player object.</param>
        /// <returns>GameResults object.</returns>
        public static GameResults PlayGame(UserPlayer user, ComputerPlayer comp)
        {
            Console.WriteLine(" * * * * * * * LET'S PLAY * * * * * * * ");
            Console.WriteLine();

            // initialize GameResults object to store the current game's stats to display at the end of the game
            GameResults results = new GameResults() { };

            var players = new List<Player>() { user, comp };

            // start the game!
            // keep playing until one of the players DeckSize is 0
            while (user.DeckSize > 0 && comp.DeckSize > 0)
            {
                // display the played card
                Console.WriteLine($" ------------ ROUND {results.NumberOfRounds} ------------ ");
                Console.WriteLine();
                Console.WriteLine($" You have played ... the { user.PlayingCard.CardFace } of { user.PlayingCard.CardSuit }.");
                Console.WriteLine($" The computer has played ... the { comp.PlayingCard.CardFace } of { comp.PlayingCard.CardSuit }.");
                Console.WriteLine();

                // compare each player's playing card
                    // first check if there is a War (war is when both players cards are the same value)
                if (user.PlayingCard.CardValue.Equals(comp.PlayingCard.CardValue))
                {
                    // start the war!
                    War(user, comp, results);

                    // display message depending if the user won this round
                    if (user.WonWar)
                    {
                        Console.WriteLine(" **** You won this round! **** ");
                        Console.WriteLine();

                        results.NumberOfUserRoundWins++;
                        results.NumberOfUserWarWins++;
                    }
                    else
                    {
                        Console.WriteLine(" **** You lost this round... **** ");
                        Console.WriteLine();
                    }
                }
                else
                {
                    // determine who won this round
                    user.WonRound = user.PlayingCard.CardValue > comp.PlayingCard.CardValue;
                    comp.WonRound = !user.WonRound;

                    // get the winner and loser of this round
                    var winnerOfRound = players.FirstOrDefault(p => p.WonRound);
                    var loserOfRound = players.FirstOrDefault(p => !p.WonRound);

                    // for the winning player of the round - they get the loser's playing card
                    var loserPlayingCard = loserOfRound.PlayingCard;
                    winnerOfRound.Deck.Add(loserPlayingCard);
                    loserOfRound.Deck.Remove(loserPlayingCard);

                    // move winner's first card on deck (their playing card) to the bottom of their deck
                    var winnerPlayingCard = winnerOfRound.PlayingCard;
                    winnerOfRound.Deck.Remove(winnerPlayingCard);
                    winnerOfRound.Deck.Add(winnerPlayingCard);

                    // display message depending if the user won this round
                    if (user.WonRound)
                    {
                        Console.WriteLine(" **** You won this round! **** ");
                        Console.WriteLine();

                        results.NumberOfUserRoundWins++;
                    }
                    else
                    {
                        Console.WriteLine(" **** You lost this round... **** ");
                        Console.WriteLine();
                    }
                }

                // keep track of how many rounds played until finish
                if (user.DeckSize > 0 && comp.DeckSize > 0)
                {
                    results.NumberOfRounds++;
                }

                Console.WriteLine($" --- Number of cards in your deck: {user.DeckSize}");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }

            // exiting the loop means a player no longer has cards in their deck - determine who the winner is
            user.IsWinner = user.DeckSize > 0 && comp.DeckSize < 1;
            comp.IsWinner = !user.IsWinner;

            return results;
        }
    }
}
