using PocketDictionary.Views;

namespace PocketDictionary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent(); 
            Routing.RegisterRoute(nameof(DeckDetailsPage), typeof(DeckDetailsPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CreateFlashcardPage), typeof(CreateFlashcardPage));
            Routing.RegisterRoute(nameof(FlashcardDetailsPage), typeof(FlashcardDetailsPage));
            Routing.RegisterRoute(nameof(ReviewPage), typeof(ReviewPage));
        }
    }
}
