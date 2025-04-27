using Microsoft.Maui.Controls;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel;

        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }
        // Override OnAppearing to refresh deck data when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh the deck
            _viewModel.LoadDecks();
        }
    }
}