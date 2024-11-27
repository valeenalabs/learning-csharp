using System;

namespace DesktopMathGame.Models;

public record GameRound
{
    public int MinNumber { get; set; } = 1;
    public int MaxNumber { get; set; } = 30;
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public int CorrectAnswer { get; set; }
    public int UserAnswer { get; set; }
    public bool IsAnswerCorrect() { return UserAnswer == CorrectAnswer; }
    public char? OperationSymbol { get; set; }
    public GameTypeEnum GameType { get; set; }
    public TimeSpan RoundTime { get; set; }
    
}