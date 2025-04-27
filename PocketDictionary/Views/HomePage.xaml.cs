using Microsoft.Maui.Controls;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class with a specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to bind to the page.</param>
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        /// <summary>
        /// Called when the page appears. Refreshes the deck data.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadDecks();
        }
    }
}