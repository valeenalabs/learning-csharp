namespace ConsoleMathGame;

public class GameRound
{
    public int MinNumber { get; set; } = 1;
    public int MaxNumber { get; set; } = 10;
    
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public int CorrectAnswer { get; set; }
    public int UserAnswer { get; set; }
    public bool IsCorrect() { return UserAnswer == CorrectAnswer; }

    public char? OperationSymbol;
    public GameType Type { get; set; }
    public TimeSpan TimeTaken { get; set; }
}