using SQLite;

namespace PocketDictionary.Models;

/// <summary>
/// Represents a deck of flashcards used for learning and spaced repetition.
/// </summary>
public class Deck
{
    /// <summary>
    /// Gets or sets the unique identifier for the deck.
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the deck.
    /// </summary>
    public string Name { get; set; } 

    /// <summary>
    /// Gets or sets the date and time when the deck was last opened.
    /// Defaults to the current date and time.
    /// </summary>
    public DateTime LastOpened { get; set; } = DateTime.Now;

    [Ignore]
    public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}
