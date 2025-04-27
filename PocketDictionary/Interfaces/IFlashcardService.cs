using PocketDictionary.Models;

namespace PocketDictionary;
public interface IFlashcardService
{
    void AddFlashcard(Flashcard flashcard);
    List<Flashcard> GetAllFlashcards();
    void UpdateFlashcard(Flashcard flashcard);
    void DeleteFlashcard(int id);
    List<Flashcard> GetDueFlashcards();
}
