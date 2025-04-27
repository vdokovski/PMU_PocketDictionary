using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketDictionary.Models;
using PocketDictionary.ViewModels;
using PocketDictionary.Services;

namespace PocketDictionary.Views
{
    public partial class ReviewPage : ContentPage
    {
        private readonly ReviewPageViewModel _viewModel;

        public ReviewPage(ReviewPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Make sure swipe gestures work properly
            CardSwipeView.SwipeStarted += OnSwipeStarted;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CardSwipeView.SwipeStarted -= OnSwipeStarted;
        }

        private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            // Optional: You can add visual feedback when swipe starts
        }

        public async void OnFlipInvoked(object sender, EventArgs e)
        {
            if (!_viewModel.HasBeenFlipped)
            {
                // Perform flip animation
                await FlipCardAnimation();
            }
        }

        private async Task FlipCardAnimation()
        {
            // First half of the animation - scale down horizontally
            await CardFrame.ScaleXTo(0, 100, Easing.CubicIn);
            
            // Execute the command - this changes the text on the card
            _viewModel.FlipCardCommand.Execute(null);
            
            // Second half of the animation - scale back up
            await CardFrame.ScaleXTo(1, 100, Easing.CubicOut);
        }
    }
}