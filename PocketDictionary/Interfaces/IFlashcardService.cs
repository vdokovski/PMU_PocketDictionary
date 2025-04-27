using PocketDictionary.Models;

namespace PocketDictionary;
/// <summary>
/// Provides methods for managing flashcards, including adding, retrieving, updating, and deleting flashcards.
/// </summary>
public interface IFlashcardService
{
    /// <summary>
    /// Adds a new flashcard to the collection.
    /// </summary>
    /// <param name="flashcard">The flashcard to add.</param>
    void AddFlashcard(Flashcard flashcard);

    /// <summary>
    /// Retrieves all flashcards from the collection.
    /// </summary>
    /// <returns>A list of all flashcards.</returns>
    List<Flashcard> GetAllFlashcards();

    /// <summary>
    /// Updates an existing flashcard in the collection.
    /// </summary>
    /// <param name="flashcard">The flashcard with updated information.</param>
    void UpdateFlashcard(Flashcard flashcard);

    /// <summary>
    /// Deletes a flashcard from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the flashcard to delete.</param>
    void DeleteFlashcard(int id);

    /// <summary>
    /// Retrieves flashcards that are due for review based on their next review date.
    /// </summary>
    /// <returns>A list of flashcards that are due for review.</returns>
    List<Flashcard> GetDueFlashcards();
}
