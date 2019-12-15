using System;
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
            var players = new List<Tuple<string, int>>();

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
                players.Add(new Tuple<string, int>(name, 0));
            }

            switch (gameType)
            {
                case GameTypes.Pig:
                    break;
                case GameTypes.BigPig:
                    throw new NotImplementedException();
                case GameTypes.Hog:
                    throw new NotImplementedException();
                case GameTypes.TwoDicePig:
                    throw new NotImplementedException();
                default:
                    throw new Exception($"Unexpected gameType: {gameType.ToString()}");
            }

        }

        /// <summary>
        /// Plays with default pig rules
        /// </summary>
        /// <returns>Name of winning player</returns>
        private static string PlayPig()
        {
            return string.Empty;
        }
    }
}
