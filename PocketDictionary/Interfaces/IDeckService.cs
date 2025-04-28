using PocketDictionary.Models;

namespace PocketDictionary.Interfaces;

/// <summary>
/// Provides methods for managing decks of flashcards.
/// </summary>
public interface IDeckService
{
    /// <summary>
    /// Retrieves all decks.
    /// </summary>
    /// <returns>A list of all decks.</returns>
    List<Deck> GetAllDecks();

    /// <summary>
    /// Adds a new deck.
    /// </summary>
    /// <param name="deck">The deck to add.</param>
    void AddDeck(Deck deck);

    /// <summary>
    /// Updates an existing deck.
    /// </summary>
    /// <param name="deck">The deck to update.</param>
    void UpdateDeck(Deck deck);

    /// <summary>
    /// Deletes a deck by its unique identifier.
    /// </summary>
    /// <param name="deckId">The unique identifier of the deck to delete.</param>
    void DeleteDeck(int deckId);

    /// <summary>
    /// Retrieves a deck by its unique identifier.
    /// </summary>
    /// <param name="deckId">The unique identifier of the deck to retrieve.</param>
    /// <returns>The requested deck.</returns>
    Deck GetDeck(int deckId);

    /// <summary>
    /// Loads all flashcards associated with the specified deck.
    /// </summary>
    /// <param name="deck">The deck for which to load flashcards.</param>
    void LoadFlashcards(Deck deck);
}
