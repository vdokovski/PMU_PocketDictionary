using PocketDictionary.ViewModels;

namespace PocketDictionary.Views;

public partial class FlashcardDetailsPage : ContentPage
{
    public FlashcardDetailsPage(FlashcardDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
} 