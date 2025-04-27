using Microsoft.Maui.Controls;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    public partial class CreateFlashcardPage : ContentPage
    {
        public CreateFlashcardPage(CreateFlashcardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}