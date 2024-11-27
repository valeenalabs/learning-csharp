using Avalonia.Media;

namespace DesktopMathGame.Common;

public static class GameConstants
{
    public const int AbsoluteMinNumber = int.MinValue;
    public const int AbsoluteMaxNumber = int.MaxValue;

    public const int DefaultMinNumber = 1;
    public const int DefaultMaxNumber = 30;

    public static class Colors
    {
        public static readonly IBrush Correct = Brushes.Green;
        public static readonly IBrush Incorrect = Brushes.Red;
        public static readonly IBrush Invalid = Brushes.Orange;
        public static readonly IBrush Default = Brushes.Black;
    }
}