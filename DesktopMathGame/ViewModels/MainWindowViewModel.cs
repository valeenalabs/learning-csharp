using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopMathGame.Models;

// ReSharper disable InconsistentNaming

namespace DesktopMathGame.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? currentView;
    [ObservableProperty] private GameTypeEnum gameType;
    
    // Store our game instance
    private readonly GameViewModel? gameViewModel;

    public MainWindowViewModel()
    {
        gameViewModel = new GameViewModel();
        gameViewModel.RequestNavigateHome += (_, _) => CurrentView = null;
        CurrentView = null;
    }

    public IRelayCommand StartNewGameCommand => new RelayCommand<GameTypeEnum>(gameTypeEnum =>
    {
        gameViewModel!.GenerateNewRound(gameTypeEnum);
        CurrentView = gameViewModel;
    });
}
