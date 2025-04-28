using Microsoft.Maui.Controls;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    /// <summary>
    /// Represents the page for creating a flashcard.
    /// </summary>
    public partial class CreateFlashcardPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFlashcardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model for creating flashcards.</param>
        public CreateFlashcardPage(CreateFlashcardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}