using System;

namespace NeuronNotes.Models;

public record Note
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    public Note(Guid id)
    {
        Id = id;
        Title = $"New Note {Random.Shared.Next(1, 100)}";
        Content = "This is a new note.";
        CreatedAt = DateTime.Now;
        ModifiedAt = DateTime.Now;
    }
}