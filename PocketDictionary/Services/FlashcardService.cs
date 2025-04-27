using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using SQLite;

namespace PocketDictionary.Services;
/// <summary>
/// Service for managing flashcards, including CRUD operations and retrieval of due flashcards.
/// </summary>
public class FlashcardService : IFlashcardService
{
    /// <summary>
    /// The SQLite asynchronous connection used for database operations.
    /// </summary>
    private readonly SQLiteConnection _db;

    public FlashcardService(IDbService dbService)
    {
        _db = dbService.GetConnection();
    }

    /// <summary>
    /// Adds a new flashcard to the database.
    /// </summary>
    /// <param name="card">The flashcard to add.</param>
    public void AddFlashcard(Flashcard card)
    {
        _db.Insert(card);
    }

    /// <summary>
    /// Deletes a flashcard from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the flashcard to delete.</param>
    public void DeleteFlashcard(int id)
    {
        var flashcard = _db.Find<Flashcard>(id);
        if (flashcard != null)
            _db.Delete(flashcard);
    }

    /// <summary>
    /// Retrieves all flashcards from the database.
    /// </summary>
    /// <returns>A list of all flashcards stored in the database.</returns>
    public List<Flashcard> GetAllFlashcards()
    {
        return _db.Table<Flashcard>().ToList();
    }

    /// <summary>
    /// Retrieves a list of flashcards that are due for review.
    /// </summary>
    /// <returns>A list of flashcards with a NextReviewDate less than or equal to the current date and time.</returns>
    public List<Flashcard> GetDueFlashcards()
    {
        return _db.Table<Flashcard>()
            .Where(card => card.NextReviewDate <= DateTime.Now)
            .ToList();
    }

    /// <summary>
    /// Updates an existing flashcard in the database.
    /// </summary>
    /// <param name="card">The flashcard to update.</param>
    public void UpdateFlashcard(Flashcard card)
    {
        _db.Update(card);
    }
}
