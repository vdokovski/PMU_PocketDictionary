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

        /// <summary>  
        /// Initializes a new instance of the <see cref="DeckDetailsPage"/> class.  
        /// </summary>  
        /// <param name="viewModel">The ViewModel for managing deck details.</param>  
        public DeckDetailsPage(DeckDetailsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        /// <summary>  
        /// Called when the page appears. Refreshes the deck data.  
        /// </summary>  
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadDeck();
        }
    }
}