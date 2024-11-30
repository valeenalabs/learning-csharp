using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using NeuronNotes.Models;
using NeuronNotes.Services;
using NeuronNotes.ViewModels;
using NeuronNotes.Views;
using Serilog;

namespace NeuronNotes;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            using var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            var messenger = WeakReferenceMessenger.Default;
            var fileService = new FileService(logger);
            var noteService = new NoteService(fileService, logger);

            desktop.MainWindow = new MainWindowView
            {
                DataContext = new MainWindowViewModel(noteService, messenger, logger),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}