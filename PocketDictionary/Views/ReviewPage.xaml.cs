using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    public partial class ReviewPage : ContentPage
    {
        readonly ReviewPageViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model for the review page.</param>
        public ReviewPage(ReviewPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        /// <summary>
        /// Handles the swipe gesture to flip the card.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The swipe event arguments.</param>
        void OnSwiped(object sender, SwipedEventArgs e)
        {
            if ((e.Direction == SwipeDirection.Left || e.Direction == SwipeDirection.Right)
                && !_viewModel.HasBeenFlipped)
            {
                _ = FlipCardAnimation();
            }
        }

        /// <summary>
        /// Animates the flipping of the card.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        async Task FlipCardAnimation()
        {
            await CardFrame.ScaleXTo(0, 100, Easing.CubicIn);
            _viewModel.FlipCardCommand.Execute(null);
            await CardFrame.ScaleXTo(1, 100, Easing.CubicOut);
        }
    }
}
