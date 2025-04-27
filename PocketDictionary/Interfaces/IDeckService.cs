using PocketDictionary.Models;

namespace PocketDictionary.Interfaces;

/// <summary>
/// Provides methods for managing decks of flashcards.
/// </summary>
public interface IDeckService
{
    /// <summary>
    /// Retrieves all decks asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all decks.</returns>
    List<Deck> GetAllDecks();

    /// <summary>
    /// Adds a new deck asynchronously.
    /// </summary>
    /// <param name="deck">The deck to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    void AddDeck(Deck deck);

    /// <summary>
    /// Updates an existing deck asynchronously.
    /// </summary>
    /// <param name="deck">The deck to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    void UpdateDeck(Deck deck);

    /// <summary>
    /// Deletes a deck asynchronously by its unique identifier.
    /// </summary>
    /// <param name="deckId">The unique identifier of the deck to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    void DeleteDeck(int deckId);

    /// <summary>
    /// Gets a deck asynchronously by its unique identifier.
    /// </summary>
    /// <param name="deckId">The unique identifier of the deck to get.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Deck GetDeck(int deckId);

    void LoadFlashcards(Deck deck);
}
