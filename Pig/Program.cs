﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Pig
{
    class Program
    {
        static void Main(string[] args)
        {
            GameTypes gameType = GameTypes.Invalid;
            var playerCount = 0;
            var players = new List<Player>();

            do
            {
                Console.WriteLine("Available game modes:");
                Console.WriteLine(String.Join(", ",Enum.GetValues(typeof(GameTypes)).Cast<GameTypes>().Select(x=>x.ToString())));
                Console.WriteLine("Which game type would you like to play?");

                var userSelection = Console.ReadLine();

                Enum.TryParse<GameTypes>(userSelection,out gameType);
                //TODO check here for bugs
                if (gameType == GameTypes.Invalid)
                Console.WriteLine("Invalid Selection");
                Console.WriteLine("Make another selection.");

            } while (gameType == GameTypes.Invalid);

            do
            {
                Console.WriteLine("How many players? (2-10)");

                var userSelection = Console.ReadLine();
                int.TryParse(userSelection, out playerCount);
                if (playerCount > 10 || playerCount < 2)
                {
                    playerCount = 0;
                    Console.WriteLine("Invalid number of players selected.");
                }

            } while (playerCount == 0);

            for (int i = 1; i <= playerCount; i++)
            {
                var name = string.Empty;

                do
                {
                    Console.WriteLine($"Enter player {i.ToString()}'s name: ");
                    name = Console.ReadLine();
                } while (name.Equals(string.Empty));
                players.Add(new Player() { Name=name,Score=0 });
            }

            try
            {
                switch (gameType)
                {
                    case GameTypes.Pig:
                        PlayPig(players);
                        break;
                    case GameTypes.BigPig:
                        throw new NotImplementedException();
                    case GameTypes.Hog:
                        throw new NotImplementedException();
                    case GameTypes.TwoDicePig:
                        PlayDoublePig(players);
                        break;
                    default:
                        throw new Exception($"Unexpected gameType: {gameType.ToString()}");
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Plays with default pig rules
        /// </summary>
        /// <returns>Name of winning player</returns>
        private static string PlayPig(List<Player> players)
        {
            var winner = string.Empty;

            do
            {
                for (int i = 0; i < players.Count; i++)
                {
                    var playerHolder = players.ElementAt(i);
                    TakeTurn_Pig(ref playerHolder);
                    if (playerHolder.Score >= 100)
                    {
                        winner = playerHolder.Name;
                        break;
                    }
                }
                
            } while (winner.Equals(string.Empty));

            return winner;
        }

        private static void TakeTurn_Pig(ref Player player)
        {
            var userInput = string.Empty;
            var isStillPlayerTurn = true;
            do
            {
                do
                {
                    Console.WriteLine("Roll (r) or Pass (p)?");
                    Console.WriteLine($"Your current score is {player.Score}");
                    userInput = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine("");


                    if (!userInput.Equals("r") && !userInput.Equals("p"))
                    {
                        userInput = string.Empty;
                    }
                    else
                    {
                        switch (userInput)
                        {
                            case "r":
                                var roll = Die.Roll();
                                Console.WriteLine($"{player.Name} rolled a {roll.ToString()}.");
                                if (roll == 1)
                                {
                                    player.Score = 0;
                                    isStillPlayerTurn = false;
                                }
                                else
                                {
                                    player.Score += roll;
                                }
                                break;
                            case "p":
                                isStillPlayerTurn = false;
                                break;
                            default:
                                throw new Exception("Bad state due to unexpected user input.");
                        }
                    }
                } while (userInput == string.Empty);
            } while (isStillPlayerTurn);
            Console.WriteLine($"{player.Name}'s turn has ended with a score of {player.Score}.");
        }

        private static string PlayDoublePig(List<Player> players)
        {
            var winner = string.Empty;

            do
            {
                for (int i = 0; i < players.Count; i++)
                {
                    var playerHolder = players.ElementAt(i);
                    TakeTurn_DoublePig(ref playerHolder);
                    if (playerHolder.Score >= 100)
                    {
                        winner = playerHolder.Name;
                        break;
                    }
                }
            } while (winner.Equals(string.Empty));

            return winner;
        }

        private static void TakeTurn_DoublePig(ref Player player)
        {
            var userInput = string.Empty;
            var isStillPlayerTurn = true;
            var turnScore = 0;

            do
            {
                do
                {
                    Console.WriteLine("Roll (r) or Pass (p)?");
                    Console.WriteLine($"Your current score is {player.Score + turnScore}");
                    userInput = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine("");


                    if (!userInput.Equals("r") && !userInput.Equals("p"))
                    {
                        userInput = string.Empty;
                    }
                    else
                    {
                        switch (userInput)
                        {
                            case "r":
                                var roll1 = Die.Roll();
                                var roll2 = Die.Roll();
                                Console.WriteLine($"{player.Name} rolled {roll1.ToString()}, {roll2.ToString()}.");
                                if (roll1 == 1 && roll2 == 1)
                                {
                                    player.Score = 0;
                                    isStillPlayerTurn = false;
                                }
                                else if (roll1 == 1 || roll2 == 1)
                                {
                                    turnScore = 0;
                                    isStillPlayerTurn = false;
                                }
                                else
                                {
                                    turnScore += (roll1+roll2);
                                }
                                break;
                            case "p":
                                isStillPlayerTurn = false;
                                break;
                            default:
                                throw new Exception("Bad state due to unexpected user input.");
                        }
                    }
                } while (userInput == string.Empty);
            } while (isStillPlayerTurn);
            player.Score += turnScore;
            Console.WriteLine($"{player.Name}'s turn has ended with a score of {player.Score}.");
        }

        private static class Die
        {
            private static Random random;
            static Die() { random = new Random(); }

            public static int Roll() {
                return random.Next(6) + 1;
            }
        }

        private class Player {
            public string Name { get; set; }
            public int Score { get; set; }
        }

    }
}
