

// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NeuronNotes.Models;
using NeuronNotes.Services;
using Serilog;

namespace NeuronNotes.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    [ObservableProperty] private ObservableCollection<Note> displayNotes;
    [ObservableProperty] private Guid noteId = Guid.Empty;
    [ObservableProperty] private Note? activeNote;
    [ObservableProperty] private Note? selectedNote;
    
    private readonly IMessenger _messenger;
    private readonly INoteService _noteService;
    private readonly ILogger _logger;

    public MainWindowViewModel(INoteService noteService, IMessenger messenger, ILogger logger) 
    {
        _logger = logger;
        _noteService = noteService;
        _messenger = messenger;
        DisplayNotes = new ObservableCollection<Note>(noteService.GetAllNotes().Values);
    }

    partial void OnSelectedNoteChanged(Note? value)
    {
        ActiveNote = value;
    }

    [RelayCommand]
    private void CreateNote()
    {
        Console.WriteLine("Creating note");
        var newNote = _noteService.CreateNote();
        DisplayNotes.Add(newNote);
        ActiveNote = newNote;
    }

    [RelayCommand]
    private void SaveNote()
    {
        
    }

    [RelayCommand]
    private void DeleteNote()
    {
        
    }

    [RelayCommand]
    private void NoteInListLeftClick()
    {
        
    }

    [RelayCommand]
    private void NoteInListRightClick()
    {
        
    }
    
    
}