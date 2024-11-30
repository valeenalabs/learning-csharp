using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeuronNotes.Models;

namespace NeuronNotes.Services.Interface;

public interface IFileService
{
    Task SaveNoteAsync (Note note);
    Task<Note?> LoadNoteAsync (Guid id);
    Task SaveAllNotesAsync (IEnumerable<Note> notes);
    Task<IEnumerable<Note>> LoadAllNotesAsync ();
}