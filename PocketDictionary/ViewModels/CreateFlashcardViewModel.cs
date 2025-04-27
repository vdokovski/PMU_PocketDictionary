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

    public CreateFlashcardViewModel(IDeckService deckService, IFlashcardService flashcardService)
    {
        _deckService = deckService;
        _flashcardService = flashcardService;
        _flashcard = new Flashcard();
    }

    public Deck Deck
    {
        get => _deck;
        set => SetProperty(ref _deck, value);
    }

    public Flashcard Flashcard
    {
        get => _flashcard;
        set => SetProperty(ref _flashcard, value);
    }

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

        // Navigate back to deck details page
        await Shell.Current.Navigation.PopAsync();
    }
}
