using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopMathGame.Common;
using DesktopMathGame.Models;
using DesktopMathGame.Services;

namespace DesktopMathGame.ViewModels;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty] private GameTypeEnum gameType;
    [ObservableProperty] private string equation = string.Empty;
    [ObservableProperty] private string userAnswer = string.Empty;
    [ObservableProperty] private string feedbackMessage = string.Empty;
    [ObservableProperty] private IBrush feedbackColor = GameConstants.Colors.Default;

    private readonly GameRound _gameRound = new();

    public EventHandler? RequestNavigateHome;
    
    public IRelayCommand<GameTypeEnum> GenerateNewProblemCommand { get; }
    public IRelayCommand CheckAnswerCommand { get; }
    public IRelayCommand NavigateHomeCommand { get; }

    public GameViewModel()
    {
        GenerateNewProblemCommand = new RelayCommand<GameTypeEnum>(GenerateNewRound);
        CheckAnswerCommand = new RelayCommand(CheckAnswer);
        NavigateHomeCommand = new RelayCommand(NavigateHome);
    }
    
    public void GenerateNewRound(GameTypeEnum type)
    {
        _gameRound.GameType = type;
        (_gameRound.Number1, _gameRound.Number2) = GameService.GenerateNumbers(
            type,
            GameConstants.DefaultMinNumber,
            GameConstants.DefaultMaxNumber);

        _gameRound.OperationSymbol = GameService.GetOperator(type);
        _gameRound.CorrectAnswer = GameService.CalculateAnswer(type, _gameRound.Number1, _gameRound.Number2);
        Equation = GameService.FormatEquation(type, _gameRound.Number1, _gameRound.Number2);
        
        // Reset UI state
        UserAnswer = string.Empty;
        FeedbackMessage = string.Empty;
        FeedbackColor =  GameConstants.Colors.Default;
    }
    
    private void NavigateHome()
    {
        RequestNavigateHome?.Invoke(this, EventArgs.Empty);
    }

    private void CheckAnswer()
    {
        if (string.IsNullOrWhiteSpace(UserAnswer))
        {
            FeedbackMessage = "Please enter an answer";
            FeedbackColor = GameConstants.Colors.Invalid;
            return;
        }   
        
        var feedback = GameService.ValidateAnswer(UserAnswer, _gameRound.CorrectAnswer);
        FeedbackMessage = feedback.Message;
        FeedbackColor = feedback.Color;
    }

}
    