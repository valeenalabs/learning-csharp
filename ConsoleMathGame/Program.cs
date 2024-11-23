using System.Text.RegularExpressions;

namespace ConsoleMathGame;

public static class Program
{
    
    private static void Main(string[] args)
    {
        var mathGame = new MathGame();
        var score = new List<Score>();
        while (true)
        {
            Console.WriteLine($"""
                              Welcome to Valeena's Math Game, Console Version
                              ----------------------------------------------
                              Please choose one of the following options:
                                      a. Addition Game
                                      s. Subtraction Game
                                      m. Multiplication Game
                                      d. Division Game
                                      h. Show Score History
                                      q. Exit
                              ----------------------------------------------
                              """);
        
            var gameSelected = Console.ReadLine();
            if (gameSelected == null || !Regex.IsMatch(gameSelected.ToLower(), "[a|s|m|d|h|q|e]"))
            {
                Console.WriteLine("Please enter a valid game selection.");
            } 
            else
            {
                switch (gameSelected.ToLower())
                {
                    case "a":
                        mathGame.PlayGame(GameType.Addition, score);
                        break;
                    case "s":
                        mathGame.PlayGame(GameType.Subtraction, score);
                        break;
                    case "m":
                        mathGame.PlayGame(GameType.Multiplication, score);
                        break;
                    case "d":
                        mathGame.PlayGame(GameType.Division, score);
                        break;
                    case "h":
                        if (score.Count == 0)
                            Console.WriteLine("No history available.");
                        else
                        {
                            Console.Clear();
                            DisplayHistory(score);
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        break;
                    case "e":
                    case "q":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }

    private static void DisplayHistory(List<Score> score)
    {
        foreach (var game in score)
        {
            Console.WriteLine($"""
                               Equation: {game.Equation}
                               Result: {game.EquationResult}
                               Win: {game.Win}
                               """);
        }
    }
}
