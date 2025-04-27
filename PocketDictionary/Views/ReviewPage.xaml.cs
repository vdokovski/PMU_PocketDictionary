using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using PocketDictionary.ViewModels;

namespace PocketDictionary.Views
{
    public partial class ReviewPage : ContentPage
    {
        readonly ReviewPageViewModel _viewModel;

        public ReviewPage(ReviewPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        // SwipeGestureRecognizer invokes this
        void OnSwiped(object sender, SwipedEventArgs e)
        {
            // Only flip if left or right, and not already flipped
            if ((e.Direction == SwipeDirection.Left || e.Direction == SwipeDirection.Right)
                && !_viewModel.HasBeenFlipped)
            {
                _ = FlipCardAnimation();
            }
        }

        async Task FlipCardAnimation()
        {
            // scale down
            await CardFrame.ScaleXTo(0, 100, Easing.CubicIn);

            // flip in VM
            _viewModel.FlipCardCommand.Execute(null);

            // scale back up
            await CardFrame.ScaleXTo(1, 100, Easing.CubicOut);
        }
    }
}
