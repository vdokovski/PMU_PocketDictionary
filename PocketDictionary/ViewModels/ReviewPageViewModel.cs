using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PocketDictionary.Models; // Assuming your Flashcard model is there

namespace PocketDictionary.ViewModels
{
    public class ReviewPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Deck Deck { get; }
        private int _currentIndex = 0;
        private bool _isFrontVisible = true;

        public ObservableCollection<Flashcard> FlashcardsToReview { get; }

        public ICommand FlipCardCommand { get; }
        public ICommand KnowCommand { get; }
        public ICommand DontKnowCommand { get; }

        public ReviewPageViewModel(Deck deck)
        {
            Deck = deck;
            FlashcardsToReview = new ObservableCollection<Flashcard>(deck.Flashcards);

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

        public string RemainingFlashcardsText => $"{FlashcardsToReview.Count - _currentIndex} left";

        private void UpdateCurrentFlashcard()
        {
            if (_currentIndex < FlashcardsToReview.Count)
            {
                CurrentFlashcard = FlashcardsToReview[_currentIndex];
                _isFrontVisible = true;
                OnPropertyChanged(nameof(RemainingFlashcardsText));
            }
            else
            {
                // TODO: Handle end of review
            }
        }

        private void FlipCard()
        {
            _isFrontVisible = !_isFrontVisible;
            OnPropertyChanged(nameof(CurrentFlashcardText));
        }

        private void KnowCard()
        {
            _currentIndex++;
            UpdateCurrentFlashcard();
        }

        private void DontKnowCard()
        {
            _currentIndex++;
            UpdateCurrentFlashcard();
        }
    }
}