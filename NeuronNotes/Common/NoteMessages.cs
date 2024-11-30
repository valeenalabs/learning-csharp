using NeuronNotes.Models;

namespace NeuronNotes.Common;

public record NoteSelectedMessage(Note? Note);
public record NoteCreatedMessage(Note? Note);
public record NoteDeletedMessage(Note? Note);
public record NoteModifiedMessage(Note? Note);