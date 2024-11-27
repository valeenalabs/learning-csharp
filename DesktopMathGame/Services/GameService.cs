using System;
using DesktopMathGame.Common;
using DesktopMathGame.Models;

namespace DesktopMathGame.Services;

public static class GameService
{
    /// <summary>
    /// Generates two numbers for a math problem
    /// For division, ensure the result is a whole number
    /// </summary>
    /// <param name="type">The type of math operation to generate numbers for</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>
    /// A tuple containing two integers (number1, number2) where:
    /// - For division: number1 is the product, number2 is the divisor
    /// - For other operations: both numbers are random within the specified range
    /// </returns>
    public static (int number1, int number2) GenerateNumbers(GameTypeEnum type, int min, int max)
    {
        if (type != GameTypeEnum.Division) return (Random.Shared.Next(min, max), Random.Shared.Next(min, max));
        var divisor = Random.Shared.Next(min, max);
        var product = divisor * Random.Shared.Next(min, max);
        return (product, divisor);

    }

    /// <summary>
    /// Calculates the answer of an equation
    /// </summary>
    /// <param name="type">Type of math operation</param>
    /// <param name="number1">First number</param>
    /// <param name="number2">Second number</param>
    /// <returns>Result of the equation based on the type</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static int CalculateAnswer(GameTypeEnum type, int number1, int number2)
    {
        try
        {
            return type switch
            {
                GameTypeEnum.Addition => number1 + number2,
                GameTypeEnum.Subtraction => number1 - number2,
                GameTypeEnum.Multiplication => number1 * number2,
                GameTypeEnum.Division => number1 / number2,
                _ => throw new ArgumentException("Invalid game type", nameof(type))
            };
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e);
            throw new InvalidOperationException("Cannot divide by zero");
        }
    }

    /// <summary>
    /// Compares the user answer to the correct answer and check if the answer is valid
    /// </summary>
    /// <param name="userInput">User answer</param>
    /// <param name="correctAnswer">Correct answer</param>
    /// <returns>
    /// GameFeedback
    /// </returns>
    public static GameFeedback ValidateAnswer(string userInput, int correctAnswer)
    {
        if (!int.TryParse(userInput, out var parsedAnswer))
        {
            return new GameFeedback
            {
                IsValid = false,
                IsCorrect = false,
                Message = "Please enter a valid number",
                Color = GameConstants.Colors.Invalid
            };
        }
        return new GameFeedback
        {
            IsValid = true,
            IsCorrect = parsedAnswer == correctAnswer,
            Message = parsedAnswer == correctAnswer
                ? "Correct! Well done!"
                : $"Not quite - the answer was {correctAnswer}",
            Color = parsedAnswer == correctAnswer
            ? GameConstants.Colors.Correct
            : GameConstants.Colors.Incorrect
        };
    }

    /// <summary>
    /// Creates a string based on the numbers and the operator
    /// </summary>
    /// <param name="type"></param>
    /// <param name="number1"></param>
    /// <param name="number2"></param>
    /// <returns></returns>
    public static string FormatEquation(GameTypeEnum type, int number1, int number2)
    {
        var op = GetOperator(type);
        return $"{number1} {op} {number2} = ?";
    }

    /// <summary>
    /// Returns a char representing the operator
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static char GetOperator(GameTypeEnum type) => type switch
    {
        GameTypeEnum.Addition => '+',
        GameTypeEnum.Subtraction => '-',
        GameTypeEnum.Multiplication => '*',
        GameTypeEnum.Division => '/',
        _ => throw new ArgumentException("Invalid game type", nameof(type))
    };

}