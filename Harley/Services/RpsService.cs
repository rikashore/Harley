using System;
using Harley.Common.Attributes;

namespace Harley.Services
{
    [HarleyService]
    public class RpsService
    {
        private readonly string[] _choices;
        private readonly Random _random;

        public RpsService(Random random)
        {
            _choices = new[] {"rock", "paper", "scissors"};
            _random = random;
        }

        // Get a random choice for the game
        public string GetChoice()
            => _choices[_random.Next(_choices.Length)];

        /*
         * Basically we're gonna do some handling to check who wins and return an integer
         * 0: Computer wins
         * 1: Player wins
         * 2: Draw
         */
        public int GetWinner(string playerChoice, string computerChoice)
        {
            switch (playerChoice)
            {
                case "rock" when computerChoice == "rock":
                case "paper" when computerChoice == "paper":
                case "scissors" when computerChoice == "scissors":
                    return 2;
                case "rock" when computerChoice == "scissors":
                case "paper" when computerChoice == "rock":
                case "scissors" when computerChoice == "paper":
                    return 1;
                case "scissors" when computerChoice == "rock":
                case "rock" when computerChoice == "paper":
                case "paper" when computerChoice == "scissors":
                    return 0;
            }

            return 0;
        }
    }
}