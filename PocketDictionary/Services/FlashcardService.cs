using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using SQLite;

namespace PocketDictionary.Services;
/// <summary>
/// Service for managing flashcards, including CRUD operations and retrieval of due flashcards.
/// </summary>
public class FlashcardService : IFlashcardService
{
    /// <inheritdoc />
    private readonly SQLiteConnection _db;

    public FlashcardService(IDbService dbService)
    {
        _db = dbService.GetConnection();
    }

    /// <inheritdoc />
    public void AddFlashcard(Flashcard card)
    {
        _db.Insert(card);
    }

    /// <inheritdoc />
    public void DeleteFlashcard(int id)
    {
        var flashcard = _db.Find<Flashcard>(id);
        if (flashcard != null)
            _db.Delete(flashcard);
    }

    /// <inheritdoc />
    public List<Flashcard> GetAllFlashcards()
    {
        return _db.Table<Flashcard>().ToList();
    }

    /// <inheritdoc />
    public List<Flashcard> GetDueFlashcards()
    {
        return _db.Table<Flashcard>()
            .Where(card => card.NextReviewDate <= DateTime.Now)
            .ToList();
    }

    /// <inheritdoc />
    public void UpdateFlashcard(Flashcard card)
    {
        _db.Update(card);
    }
}
