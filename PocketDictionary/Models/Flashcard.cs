using SQLite;

namespace PocketDictionary.Models;

/// <summary>
/// Represents a flashcard with a front and back side, used for learning and spaced repetition.
/// </summary>
public class Flashcard
{
    public Flashcard()
    {
    }

    /// <summary>
    /// Gets or sets the unique identifier for the flashcard.
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the text displayed on the front side of the flashcard.
    /// </summary>
    public string Front { get; set; }

    /// <summary>
    /// Gets or sets the text displayed on the back side of the flashcard.
    /// </summary>
    public string Back { get; set; }

    /// <summary>
    /// Gets or sets the number of consecutive correct answers for this flashcard.
    /// Used for spaced repetition logic.
    /// </summary>
    public int CorrectAnswersInARow { get; set; } = 0;

    /// <summary>
    /// Gets or sets the next review date for the flashcard.
    /// Determines when the flashcard should be reviewed again.
    /// </summary>
    public DateTime NextReviewDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the identifier of the deck to which this flashcard belongs.
    /// </summary>
    public int DeckId { get; set; }
}
