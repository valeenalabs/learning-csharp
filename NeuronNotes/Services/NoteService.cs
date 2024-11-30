using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NeuronNotes.Models;
using NeuronNotes.Services.Interface;
using Serilog;

namespace NeuronNotes.Services;

public class NoteService : INoteService
{
    public IReadOnlyDictionary<Guid, Note> GetAllNotes() => _notes;
    private readonly Dictionary<Guid, Note> _notes = new();
    
    private readonly IFileService _fileService;

    private readonly ILogger _logger;
    // Track if a note needs saving
    private readonly HashSet<Guid> _dirtyNotes = [];
    
    // Timer for auto-save
    private readonly PeriodicTimer _autoSaveTimer;
    public Note? ActiveNote { get; private set; }

    public NoteService(IFileService fileService, ILogger logger)
    {
        _logger = logger;
        _fileService = fileService;
        _autoSaveTimer = new PeriodicTimer(TimeSpan.FromSeconds(30));
        _ = RunAutoSaveLoop();
    }

    private async Task RunAutoSaveLoop()
    {
        try
        {
            while (await _autoSaveTimer.WaitForNextTickAsync())
            {
                await SaveDirtyNotesAsync();
            }
        }
        catch (OperationCanceledException)
        {
           // Operation was cancelled
           // TODO: Add logging
        }
    }

    private async Task SaveDirtyNotesAsync()
    {
        var notesToSave = _dirtyNotes.Select(id => _notes[id]).ToList();
        foreach (var note in notesToSave)
        {
            await _fileService.SaveNoteAsync(note);
            _dirtyNotes.Remove(note.Id);
        }
    }

    public Note CreateNote()
    {
        var guid = Guid.NewGuid();
        var note = new Note(guid);
        _notes.Add(guid, note);
        return note;
    }

    public void ModifyNote(Note note)
    {
        try
        {
            // Question : is it a good idea to check if there are changes or it doesn't matter ?
            if (!_notes.TryGetValue(note.Id, out var noteObject)) throw new InvalidOperationException();
            if (noteObject.Title != note.Title)
                noteObject.Title = note.Title;
            if (noteObject.Content != note.Content)
                noteObject.Content = note.Content;
            noteObject.ModifiedAt = DateTime.Now;
            _dirtyNotes.Add(note.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteNote(Note note)
    {
        try
        {
            _notes.Remove(note.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SelectNote(Note note)
    {
        if (!_notes.TryGetValue(note.Id, out var noteObject)) throw new InvalidOperationException();
        ActiveNote = noteObject;
    }
    
 
}