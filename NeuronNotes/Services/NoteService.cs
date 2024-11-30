using System;
using System.Collections.Generic;
using NeuronNotes.Models;

namespace NeuronNotes.Services;

public class NoteService : INoteService
{
    public IReadOnlyDictionary<Guid, Note> GetAllNotes() => _notes;
    private readonly Dictionary<Guid, Note> _notes = new();
    public Note? ActiveNote { get; private set; }

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
            if (!_notes.TryGetValue(note.Id, out var noteObject)) throw new InvalidOperationException();
            if (noteObject.Title != note.Title)
                noteObject.Title = note.Title;
            if (noteObject.Content != note.Content)
                noteObject.Content = note.Content;
            noteObject.ModifiedAt = DateTime.Now;
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