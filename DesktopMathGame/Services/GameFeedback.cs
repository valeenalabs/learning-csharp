using Avalonia.Media;

namespace DesktopMathGame.Services;

/// <summary>
/// Handles UI feedback in a structured way
/// </summary>
public record GameFeedback
{
    /// <summary>
    /// Can the userAnswer be parsed to int
    /// </summary>
    public required bool IsValid { get; init; }
    /// <summary>
    /// Is the userAnswer equals to correctAnswer
    /// </summary>
    public required bool IsCorrect { get; init; }
    /// <summary>
    /// Message to show in the UI
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// Color of the message
    /// </summary>
    public required IBrush Color { get; init; }
}