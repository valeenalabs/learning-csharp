using System;
using System.Collections.Generic;
using NeuronNotes.Models;

namespace NeuronNotes.Services;

public interface INoteService
{
    IReadOnlyDictionary<Guid, Note> GetAllNotes();
    Note? ActiveNote { get; }
    
    Note CreateNote();
    void ModifyNote(Note note);
    void DeleteNote(Note note);
    void SelectNote(Note note);
    

}