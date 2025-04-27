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
    [QueryProperty("Deck", "deck")]
    public partial class ReviewPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDeckService _deckService;
        private readonly IFlashcardService _flashcardService;

        private Deck _deck;
        public Deck Deck
        {
            get => _deck;
            set => SetProperty(ref _deck, value);
        }

        private int _currentIndex = 0;
        private bool _isFrontVisible = true;

        public ObservableCollection<Flashcard> FlashcardsToReview { get; }

        public ReviewPageViewModel(IFlashcardService flashcardService, IDeckService deckService)
        {
            _deckService = deckService;
            _flashcardService = flashcardService;
            Deck = _deckService.GetDeck(3);
            LoadDeck();
            FlashcardsToReview = new ObservableCollection<Flashcard>(Deck.Flashcards.Where(f => f.NextReviewDate <= DateTime.Now));

            UpdateCurrentFlashcard();
        }

        public ReviewPageViewModel()
        {
        }

        private Flashcard _currentFlashcard;
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

        public string CurrentFlashcardText => _isFrontVisible ? CurrentFlashcard?.Front : CurrentFlashcard?.Back;

        public string RemainingFlashcardsText => $"Remaining cards: {FlashcardsToReview.Count - _currentIndex}";

        private void UpdateCurrentFlashcard()
        {
            if (_currentIndex < FlashcardsToReview.Count)
            {
                CurrentFlashcard = FlashcardsToReview[_currentIndex];
                _isFrontVisible = true;
                OnPropertyChanged(nameof(CurrentFlashcardText));
                OnPropertyChanged(nameof(RemainingFlashcardsText));
            }
            else
            {
                // End of review - navigate back
                Shell.Current.Navigation.PopAsync();
            }
        }
        public void LoadDeck()
        {
            _deckService.LoadFlashcards(Deck);

            // Refresh list if needed
            OnPropertyChanged(nameof(Deck));
        }

        [RelayCommand]
        private void FlipCard()
        {
            _isFrontVisible = !_isFrontVisible;
            OnPropertyChanged(nameof(CurrentFlashcardText));
        }

        [RelayCommand]
        private void KnowCard()
        {
            if (CurrentFlashcard != null)
            {
                // Increment consecutive correct answers
                CurrentFlashcard.CorrectAnswersInARow++;
                
                // Set next review date to current date + consecutive days
                CurrentFlashcard.NextReviewDate = DateTime.Now.AddDays(CurrentFlashcard.CorrectAnswersInARow);
                
                // Save changes to database
                _flashcardService.UpdateFlashcard(CurrentFlashcard);
            }

            _currentIndex++;
            UpdateCurrentFlashcard();
        }

        [RelayCommand]
        private void DontKnowCard()
        {
            if (CurrentFlashcard != null)
            {
                // Decrement consecutive correct answers (minimum 0)
                CurrentFlashcard.CorrectAnswersInARow = Math.Max(0, CurrentFlashcard.CorrectAnswersInARow - 1);
                
                // Set next review date to current date + 2 * consecutive days
                int daysToAdd = 2 * Math.Max(1, CurrentFlashcard.CorrectAnswersInARow);
                CurrentFlashcard.NextReviewDate = DateTime.Now.AddDays(daysToAdd);
                
                // Save changes to database
                _flashcardService.UpdateFlashcard(CurrentFlashcard);
            }

            _currentIndex++;
            UpdateCurrentFlashcard();
        }
    }
}