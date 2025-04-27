using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using SQLite;

namespace PocketDictionary.Services;

/// <summary>
/// Provides methods for managing decks of flashcards.
/// </summary>
public class DeckService : IDeckService
{
    /// <summary>
    /// The SQLite asynchronous connection used for database operations.
    /// </summary>
    private readonly SQLiteConnection _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeckService"/> class with the specified database connection.
    /// </summary>
    /// <param name="db">The SQLite asynchronous connection.</param>
    public DeckService(IDbService dbService)
    {
        _db = dbService.GetConnection();
    }

    /// <inheritdoc />
    public List<Deck> GetAllDecks()
    {
        return _db.Table<Deck>().ToList();
    }

    /// <inheritdoc />
    public void AddDeck(Deck deck)
    {
        _db.Insert(deck);
    }

    /// <inheritdoc />
    public void UpdateDeck(Deck deck)
    {
        _db.Update(deck);
    }

    /// <inheritdoc />
    public void DeleteDeck(int deckId)
    {
        var deck = _db.Find<Deck>(deckId);
        if (deck != null)
            _db.Delete(deck);
    }

    public void LoadFlashcards(Deck deck)
    {
       deck.Flashcards = _db.Table<Flashcard>().Where(f => f.DeckId == deck.Id).ToList();
    }
}
