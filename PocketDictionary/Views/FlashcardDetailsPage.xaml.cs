using PocketDictionary.ViewModels;

namespace PocketDictionary.Views;

/// <summary>
/// Represents the details page for a flashcard.
/// </summary>
public partial class FlashcardDetailsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FlashcardDetailsPage"/> class.
    /// </summary>
    /// <param name="viewModel">The ViewModel for managing flashcard details.</param>
    public FlashcardDetailsPage(FlashcardDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
