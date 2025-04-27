using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using PocketDictionary.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PocketDictionary.ViewModels;

/// <summary>
/// ViewModel for managing the details of a deck, including its flashcards and related actions.
/// </summary>
[QueryProperty("Deck", "deck")]
public partial class DeckDetailsViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly IDeckService _deckService;
    private readonly IFlashcardService _flashcardService;

    private Deck _deck;

    /// <summary>
    /// Gets or sets the deck being managed by this ViewModel.
    /// </summary>
    public Deck Deck
    {
        get => _deck;
        set => SetProperty(ref _deck, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeckDetailsViewModel"/> class.
    /// </summary>
    public DeckDetailsViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeckDetailsViewModel"/> class with the specified services.
    /// </summary>
    /// <param name="deckService">The service for managing decks.</param>
    /// <param name="flashcardService">The service for managing flashcards.</param>
    public DeckDetailsViewModel(IDeckService deckService, IFlashcardService flashcardService)
    {
        _deckService = deckService;
        _flashcardService = flashcardService;
    }

    /// <summary>
    /// Loads the flashcards associated with the current deck.
    /// </summary>
    public void LoadDeck()
    {
        _deckService.LoadFlashcards(Deck);
        OnPropertyChanged(nameof(Deck));
    }

    /// <summary>
    /// Navigates to the details page of the specified flashcard.
    /// </summary>
    /// <param name="flashcard">The flashcard to view.</param>
    [RelayCommand]
    private async Task ViewFlashcardAsync(Flashcard flashcard)
    {
        if (flashcard == null)
            return;

        await Shell.Current.GoToAsync(nameof(FlashcardDetailsPage), new Dictionary<string, object>
        {
            { "flashcard", flashcard }
        });
    }

    /// <summary>
    /// Deletes the specified flashcard after user confirmation.
    /// </summary>
    /// <param name="flashcard">The flashcard to delete.</param>
    [RelayCommand]
    private async Task DeleteFlashcardAsync(Flashcard flashcard)
    {
        if (flashcard == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete Flashcard",
            $"Are you sure you want to delete '{flashcard.Front}'?",
            "Delete",
            "Cancel");

        if (confirm)
        {
            _flashcardService.DeleteFlashcard(flashcard.Id);
            LoadDeck();
        }
    }

    /// <summary>
    /// Navigates back to the home page.
    /// </summary>
    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("//HomePage");
    }

    /// <summary>
    /// Navigates to the page for creating a new flashcard in the current deck.
    /// </summary>
    [RelayCommand]
    private async Task CreateFlashcardAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateFlashcardPage), new Dictionary<string, object>
        {
            { "deck", Deck }
        });
    }

    /// <summary>
    /// Opens a modal to edit the name of the current deck.
    /// </summary>
    [RelayCommand]
    private async Task OpenEditNameModalAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(
            "Change Deck Name",
            "Enter a new name for your deck",
            "Save",
            "Cancel",
            initialValue: Deck.Name);

        if (!string.IsNullOrWhiteSpace(result))
        {
            Deck.Name = result;
            _deckService.UpdateDeck(Deck);
            OnPropertyChanged(nameof(Deck));
        }
    }

    /// <summary>
    /// Navigates to the review page for the specified deck.
    /// </summary>
    /// <param name="deck">The deck to review.</param>
    [RelayCommand]
    private async Task ReviewDeckAsync(Deck deck)
    {
        if (deck == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(ReviewPage)}?deckId={deck.Id}");
    }
}
