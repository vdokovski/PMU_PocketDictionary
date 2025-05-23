﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using PocketDictionary.Views;
using System.Collections.ObjectModel;

namespace PocketDictionary.ViewModels;

/// <summary>
/// ViewModel for the Home page, responsible for managing decks of flashcards.
/// </summary>
public partial class HomeViewModel : ObservableObject
{
    private readonly IDeckService _deckService;

    /// <summary>
    /// Gets or sets the collection of decks displayed in the Home page.
    /// </summary>
    [ObservableProperty]
    ObservableCollection<Deck> decks = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeViewModel"/> class with a specified deck service.
    /// </summary>
    /// <param name="deckService">The service used to manage decks.</param>
    public HomeViewModel(IDeckService deckService)
    {
        _deckService = deckService;
        LoadDecks();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
    /// </summary>
    public HomeViewModel()
    {
    }

    /// <summary>
    /// Loads all decks from the database and updates the <see cref="Decks"/> collection.
    /// </summary>
    public void LoadDecks()
    {
        var decksFromDb = _deckService.GetAllDecks();
        foreach (var deck in decksFromDb)
        {
            _deckService.LoadFlashcards(deck);
        }
        this.Decks = new ObservableCollection<Deck>(decksFromDb);
    }

    /// <summary>
    /// Command to add a new deck. Prompts the user for a deck name and adds it to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [RelayCommand]
    async Task AddDeck()
    {
        string result = await Shell.Current.DisplayPromptAsync("New deck", "Please enter the deck name:");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var deck = new Deck { Name = result };
            _deckService.AddDeck(deck);
            LoadDecks();
        }
    }

    /// <summary>
    /// Command to delete a deck. Prompts the user for confirmation before deleting the deck.
    /// </summary>
    /// <param name="deck">The deck to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [RelayCommand]
    async Task DeleteDeck(Deck deck)
    {
        if (deck == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete Deck",
            $"Are you sure you want to delete '{deck.Name}'?\nIt has {deck.Flashcards.Count} flashcards.",
            "Delete",
            "Cancel");

        if (confirm)
        {
            _deckService.DeleteDeck(deck.Id);
            Decks.Remove(deck);
        }
    }

    /// <summary>
    /// Command to open a deck. Navigates to the deck details page.
    /// </summary>
    /// <param name="deck">The deck to open.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [RelayCommand]
    private async Task OpenDeckAsync(Deck deck)
    {
        if (deck == null)
            return;

        await Shell.Current.GoToAsync(nameof(DeckDetailsPage), new Dictionary<string, object>
            {
                { "deck", deck }
            });
    }
}
