using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;

namespace PocketDictionary.ViewModels;

[QueryProperty("Flashcard", "flashcard")]
public partial class FlashcardDetailsViewModel : ObservableObject
{
    private readonly IFlashcardService _flashcardService;
    private Flashcard _flashcard;
    private bool _isReadOnly = true;
    private string _editButtonText = "Edit";

    public FlashcardDetailsViewModel(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    public Flashcard Flashcard
    {
        get => _flashcard;
        set => SetProperty(ref _flashcard, value);
    }

    public bool IsReadOnly
    {
        get => _isReadOnly;
        set => SetProperty(ref _isReadOnly, value);
    }

    public string EditButtonText
    {
        get => _editButtonText;
        set => SetProperty(ref _editButtonText, value);
    }

    [RelayCommand]
    private void ToggleEditMode()
    {
        if (IsReadOnly)
        {
            // Switch to edit mode
            IsReadOnly = false;
            EditButtonText = "Save";
        }
        else
        {
            // Save changes and switch back to read-only mode
            SaveChanges();
            IsReadOnly = true;
            EditButtonText = "Edit";
        }
    }

    private void SaveChanges()
    {
        // Validate input if needed
        if (string.IsNullOrWhiteSpace(Flashcard.Front) || string.IsNullOrWhiteSpace(Flashcard.Back))
        {
            return;
        }

        // Update the flashcard in the database
        _flashcardService.UpdateFlashcard(Flashcard);
    }
} 