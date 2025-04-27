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

[QueryProperty("Deck", "deck")]
public partial class DeckDetailsViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly IDeckService _deckService;
    private readonly IFlashcardService _flashcardService;

    private Deck _deck;
    public Deck Deck
    {
        get => _deck;
        set => SetProperty(ref _deck, value);
    }

    public DeckDetailsViewModel()
    {
    }
    public DeckDetailsViewModel(IDeckService deckService, IFlashcardService flashcardService)
    {
        _deckService = deckService;
        _flashcardService = flashcardService;
    }
    public void LoadDeck()
    {
        _deckService.LoadFlashcards(Deck);

        // Refresh list if needed
        OnPropertyChanged(nameof(Deck));
    }

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
    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
    [RelayCommand]
    private async Task CreateFlashcardAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateFlashcardPage), new Dictionary<string, object>
        {
            { "deck", Deck }
        });
    }
    [RelayCommand]
    private async Task OpenEditNameModalAsync()
    {
        // Show a prompt for the user to enter a new name
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
            OnPropertyChanged(nameof(Deck)); // Update the UI
        }
    }
    [RelayCommand]
    private async Task ReviewDeckAsync()
    {
        var flashcardService = Handler.MauiContext.Services.GetService<IFlashcardService>();
        await Shell.Current.Navigation.PushAsync(new ReviewPage(Deck, flashcardService));
    }
}