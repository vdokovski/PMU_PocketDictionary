using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;

namespace PocketDictionary.ViewModels
{
    [QueryProperty(nameof(DeckId), "deckId")]
    public partial class ReviewPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDeckService _deckService;
        private readonly IFlashcardService _flashcardService;

        private string _deckId;

        /// <summary>
        /// Gets or sets the DeckId. When set, it retrieves the deck and loads it.
        /// </summary>
        public string DeckId
        {
            get => _deckId;
            set
            {
                if (SetProperty(ref _deckId, value) && _deckService != null)
                {
                    Deck = _deckService.GetDeck(int.Parse(value));
                    LoadDeck();
                }
            }
        }

        private Deck _deck;

        /// <summary>
        /// Gets or sets the current deck being reviewed.
        /// </summary>
        public Deck Deck
        {
            get => _deck;
            set => SetProperty(ref _deck, value);
        }

        private int _currentIndex = 0;
        private bool _isFrontVisible = true;
        private bool _hasBeenFlipped = false;

        /// <summary>
        /// Gets a value indicating whether the current flashcard has been flipped.
        /// </summary>
        public bool HasBeenFlipped
        {
            get => _hasBeenFlipped;
            private set => SetProperty(ref _hasBeenFlipped, value);
        }

        /// <summary>
        /// Gets the collection of flashcards to review.
        /// </summary>
        public ObservableCollection<Flashcard> FlashcardsToReview { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewPageViewModel"/> class with the specified services.
        /// </summary>
        /// <param name="flashcardService">The flashcard service.</param>
        /// <param name="deckService">The deck service.</param>
        public ReviewPageViewModel(IFlashcardService flashcardService, IDeckService deckService)
        {
            _deckService = deckService;
            _flashcardService = flashcardService;
            FlashcardsToReview = new ObservableCollection<Flashcard>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewPageViewModel"/> class.
        /// </summary>
        public ReviewPageViewModel()
        {
        }

        private Flashcard _currentFlashcard;

        /// <summary>
        /// Gets or sets the current flashcard being reviewed.
        /// </summary>
        public Flashcard CurrentFlashcard
        {
            get => _currentFlashcard;
            set
            {
                _currentFlashcard = value;
                OnPropertyChanged(nameof(CurrentFlashcard));
                OnPropertyChanged(nameof(CurrentFlashcardText));
            }
        }

        /// <summary>
        /// Gets the text of the current flashcard, either the front or back depending on visibility.
        /// </summary>
        public string CurrentFlashcardText => _isFrontVisible ? CurrentFlashcard?.Front : CurrentFlashcard?.Back;

        /// <summary>
        /// Gets the text indicating the number of remaining flashcards to review.
        /// </summary>
        public string RemainingFlashcardsText => $"Remaining cards: {FlashcardsToReview.Count - _currentIndex}";

        /// <summary>
        /// Updates the current flashcard to the next one in the review list.
        /// </summary>
        private void UpdateCurrentFlashcard()
        {
            if (_currentIndex < FlashcardsToReview.Count)
            {
                CurrentFlashcard = FlashcardsToReview[_currentIndex];
                _isFrontVisible = true;
                HasBeenFlipped = false;
                OnPropertyChanged(nameof(CurrentFlashcardText));
                OnPropertyChanged(nameof(RemainingFlashcardsText));
            }
            else
            {
                Shell.Current.Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Loads the deck and prepares the flashcards for review.
        /// </summary>
        public void LoadDeck()
        {
            if (Deck != null)
            {
                _deckService.LoadFlashcards(Deck);
                FlashcardsToReview.Clear();
                foreach (var flashcard in Deck.Flashcards.Where(f => f.NextReviewDate <= DateTime.Now))
                {
                    FlashcardsToReview.Add(flashcard);
                }
                UpdateCurrentFlashcard();
                OnPropertyChanged(nameof(Deck));
            }
        }

        /// <summary>
        /// Flips the current flashcard to show the back side.
        /// </summary>
        [RelayCommand]
        private void FlipCard()
        {
            if (_isFrontVisible)
            {
                _isFrontVisible = false;
                HasBeenFlipped = true;
                OnPropertyChanged(nameof(CurrentFlashcardText));
            }
        }

        /// <summary>
        /// Marks the current flashcard as known and updates its review date.
        /// </summary>
        [RelayCommand]
        private void KnowCard()
        {
            if (CurrentFlashcard != null)
            {
                CurrentFlashcard.CorrectAnswersInARow++;
                CurrentFlashcard.NextReviewDate = DateTime.Now.AddDays(CurrentFlashcard.CorrectAnswersInARow);
                _flashcardService.UpdateFlashcard(CurrentFlashcard);
            }

            _currentIndex++;
            UpdateCurrentFlashcard();
        }

        /// <summary>
        /// Marks the current flashcard as not known and updates its review date.
        /// </summary>
        [RelayCommand]
        private void DontKnowCard()
        {
            if (CurrentFlashcard != null)
            {
                CurrentFlashcard.CorrectAnswersInARow = Math.Max(0, CurrentFlashcard.CorrectAnswersInARow - 1);
                int daysToAdd = 2 * Math.Max(1, CurrentFlashcard.CorrectAnswersInARow);
                CurrentFlashcard.NextReviewDate = DateTime.Now.AddDays(daysToAdd);
                _flashcardService.UpdateFlashcard(CurrentFlashcard);
            }

            _currentIndex++;
            UpdateCurrentFlashcard();
        }
    }
}