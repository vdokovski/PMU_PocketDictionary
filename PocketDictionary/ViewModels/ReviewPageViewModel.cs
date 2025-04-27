using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using PocketDictionary.Models;
using PocketDictionary.Services;

namespace PocketDictionary.ViewModels
{
    public class ReviewPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IFlashcardService _flashcardService;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Deck Deck { get; }
        private int _currentIndex = 0;
        private bool _isFrontVisible = true;

        public ObservableCollection<Flashcard> FlashcardsToReview { get; }

        public ICommand FlipCardCommand { get; }
        public ICommand KnowCommand { get; }
        public ICommand DontKnowCommand { get; }

        public ReviewPageViewModel(Deck deck, IFlashcardService flashcardService)
        {
            Deck = deck;
            _flashcardService = flashcardService;
            FlashcardsToReview = new ObservableCollection<Flashcard>(deck.Flashcards.Where(f => f.NextReviewDate <= DateTime.Now));

            FlipCardCommand = new RelayCommand(FlipCard);
            KnowCommand = new RelayCommand(KnowCard);
            DontKnowCommand = new RelayCommand(DontKnowCard);

            UpdateCurrentFlashcard();
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
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void FlipCard()
        {
            _isFrontVisible = !_isFrontVisible;
            OnPropertyChanged(nameof(CurrentFlashcardText));
        }

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