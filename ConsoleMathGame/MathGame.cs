using System.Text.RegularExpressions;

namespace ConsoleMathGame;

public class MathGame
{
    private readonly GameRound? _gameRound = new();

    public void PlayGame(GameType gameType, List<Score> score)
    {
        Console.Clear();
        Console.WriteLine($"""
                           You are playing the {gameType} game.
                           Do you want to customize the minimum and maximum numbers?
                           Answer with yes (y) or no (n).  
                           """);

        if (UserChoice())
        {
            Console.WriteLine("Please enter the minimum number.");
            _gameRound!.MinNumber = ParseInteger();
            Console.WriteLine("Please enter the maximum number.");
            _gameRound!.MaxNumber = ParseInteger();
        }

        _gameRound!.Type = gameType;
        _gameRound.Number2 = Random.Shared.Next(_gameRound.MinNumber, _gameRound.MaxNumber);
        _gameRound.Number1 = _gameRound.Number2  * Random.Shared.Next(_gameRound.MinNumber, _gameRound.MaxNumber);
        _gameRound.OperationSymbol = GetOperationSymbol(gameType);
        _gameRound.CorrectAnswer = CalculateAnswer();

        Console.WriteLine($"""
                           What is the result of : 
                           {_gameRound.Number1} {_gameRound.OperationSymbol} {_gameRound.Number2}
                           """);
        _gameRound.UserAnswer = ParseInteger();
        bool win;
        if (_gameRound.UserAnswer == _gameRound.CorrectAnswer)
        {
            Console.WriteLine("The answer is correct!");
            win = true;
        }
        else
        {
            Console.WriteLine("The answer is not correct! The good answer was: {_gameRound.CorrectAnswer}");
            win = false;
        }

        // Creates a string representing the equation
        var equation = $"{_gameRound.Number1} {_gameRound.OperationSymbol} {_gameRound.Number2}";
        score.Add(new Score(win, equation, _gameRound.CorrectAnswer.ToString()));

    }

    private static bool UserChoice()
    {
        while (true)
        {
            var userAnswer = Console.ReadLine()?.ToLower();
            if (userAnswer != null && Regex.IsMatch(userAnswer, "[y|yes|n|no]"))
                switch (userAnswer)
                {
                    case "yes":
                    case "y":
                        return true;
                    case "no":
                    case "n":
                        return false;
                }

            Console.WriteLine("Please enter a valid choice: Yes (Y) or No (N)");
        }
    }

    /// <summary>
    /// Parse the integer from the user input in the console.
    /// </summary>
    /// <returns>number</returns>
    private static int ParseInteger()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var number))
            {
                return number;
            }

            Console.WriteLine("Please enter a valid integer");
        }
    }


    /// <summary>
    /// Calculates the answer of the equation.
    /// </summary>
    private int CalculateAnswer()
    {
        return _gameRound!.Type switch
        {
            GameType.Addition => _gameRound.Number1 + _gameRound.Number2,
            GameType.Subtraction => _gameRound.Number1 - _gameRound.Number2,
            GameType.Multiplication => _gameRound.Number1 * _gameRound.Number2,
            GameType.Division => _gameRound.Number1 / _gameRound.Number2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Sets the operation symbol based on the GameType
    /// </summary>
    /// <returns>Operation symbol</returns>
    private static char GetOperationSymbol(GameType gameType)
    {
        return gameType switch
        {
            GameType.Addition => '+',
            GameType.Subtraction => '-',
            GameType.Multiplication => '*',
            GameType.Division => '/',
            _ => throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null)
        };
    }
}