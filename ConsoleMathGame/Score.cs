namespace ConsoleMathGame;

public class Score(bool win, string equation, string equationResult)
{
    public bool Win { get; set; } = win;
    public string? Equation  { get; set; } = equation;
    public string? EquationResult { get; set; } = equationResult;
}