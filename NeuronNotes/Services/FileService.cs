using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NeuronNotes.Models;
using NeuronNotes.Services.Interface;

namespace NeuronNotes.Services;

public class FileService : IFileService
{
    private readonly string _basePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "NeuronNotes");

    public FileService()
    {
        Directory.CreateDirectory(_basePath);
    }

    public async Task SaveNoteAsync(Note note)
    {
        var filePath = GetNoteFilePath(note.Id);
        var json = JsonSerializer.Serialize(note);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<Note?> LoadNoteAsync(Guid id)
    {
        var filePath = GetNoteFilePath(id);
        if (!File.Exists(filePath)) return null;
        
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<Note>(json);
    }

    /// <inheritdoc />
    public Task SaveAllNotesAsync(IEnumerable<Note> notes)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Note>> LoadAllNotesAsync()
    {
        throw new NotImplementedException();
    }

    private string GetNoteFilePath(Guid id) =>
        Path.Combine(_basePath, $"{id}.json");
    
}