using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;

namespace PocketDictionary.ViewModels;

/// <summary>
/// ViewModel for managing the details of a flashcard, including editing and saving changes.
/// </summary>
[QueryProperty("Flashcard", "flashcard")]
public partial class FlashcardDetailsViewModel : ObservableObject
{
    private readonly IFlashcardService _flashcardService;
    private Flashcard _flashcard;
    private bool _isReadOnly = true;
    private string _editButtonText = "Edit";

    /// <summary>
    /// Initializes a new instance of the <see cref="FlashcardDetailsViewModel"/> class.
    /// </summary>
    /// <param name="flashcardService">The service for managing flashcards.</param>
    public FlashcardDetailsViewModel(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    /// <summary>
    /// Gets or sets the flashcard being managed by this ViewModel.
    /// </summary>
    public Flashcard Flashcard
    {
        get => _flashcard;
        set => SetProperty(ref _flashcard, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the flashcard is in read-only mode.
    /// </summary>
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set => SetProperty(ref _isReadOnly, value);
    }

    /// <summary>
    /// Gets or sets the text displayed on the edit button.
    /// </summary>
    public string EditButtonText
    {
        get => _editButtonText;
        set => SetProperty(ref _editButtonText, value);
    }

    /// <summary>
    /// Toggles between edit mode and read-only mode. Saves changes when switching back to read-only mode.
    /// </summary>
    [RelayCommand]
    private void ToggleEditMode()
    {
        if (IsReadOnly)
        {
            IsReadOnly = false;
            EditButtonText = "Save";
        }
        else
        {
            SaveChanges();
            IsReadOnly = true;
            EditButtonText = "Edit";
        }
    }

    /// <summary>
    /// Saves changes to the flashcard by updating it in the database.
    /// </summary>
    private void SaveChanges()
    {
        if (string.IsNullOrWhiteSpace(Flashcard.Front) || string.IsNullOrWhiteSpace(Flashcard.Back))
        {
            return;
        }

        _flashcardService.UpdateFlashcard(Flashcard);
    }
}
