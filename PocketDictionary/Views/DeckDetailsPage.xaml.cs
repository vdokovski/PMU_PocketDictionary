using PocketDictionary.Models;
using PocketDictionary.Services;
using PocketDictionary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketDictionary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckDetailsPage : ContentPage
    {
        private readonly DeckDetailsViewModel _viewModel;
        public DeckDetailsPage(DeckDetailsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }
        // Override OnAppearing to refresh deck data when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh the deck
            _viewModel.LoadDeck();
        }
    }
}