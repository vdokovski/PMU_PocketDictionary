using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using PocketDictionary.Views;

namespace PocketDictionary.ViewModels;

/// <summary>
/// ViewModel for creating flashcards. Handles the logic for saving flashcards to the database
/// and clearing input fields after saving.
/// </summary>
[QueryProperty("Deck", "deck")]
public partial class CreateFlashcardViewModel : ObservableObject
{
    private readonly IDeckService _deckService;
    private readonly IFlashcardService _flashcardService;
    private Deck _deck;
    private Flashcard _flashcard;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFlashcardViewModel"/> class.
    /// </summary>
    /// <param name="deckService">Service for managing decks.</param>
    /// <param name="flashcardService">Service for managing flashcards.</param>
    public CreateFlashcardViewModel(IDeckService deckService, IFlashcardService flashcardService)
    {
        _deckService = deckService;
        _flashcardService = flashcardService;
        _flashcard = new Flashcard();
    }

    /// <summary>
    /// Gets or sets the deck associated with the flashcards.
    /// </summary>
    public Deck Deck
    {
        get => _deck;
        set => SetProperty(ref _deck, value);
    }

    /// <summary>
    /// Gets or sets the flashcard being created or edited.
    /// </summary>
    public Flashcard Flashcard
    {
        get => _flashcard;
        set => SetProperty(ref _flashcard, value);
    }

    /// <summary>
    /// Saves the flashcard to the database and navigates back to the previous page.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task SaveFlashcardAsync()
    {
        if (string.IsNullOrWhiteSpace(Flashcard.Front) || string.IsNullOrWhiteSpace(Flashcard.Back))
        {
            return;
        }

        Flashcard.DeckId = Deck.Id;
        _flashcardService.AddFlashcard(Flashcard);
        _deckService.LoadFlashcards(Deck);

        await Shell.Current.Navigation.PopAsync();
    }
}
