using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace facade
{
	public partial class MainPageViewModel: ObservableObject
	{
		[ObservableProperty]
		private string secretColor;

		[ObservableProperty]
		private string currentGuess;

		[ObservableProperty]
        private bool didWin;

        public ObservableCollection<ColorGuess> Guesses { get; set; }

		//public string SecretColor { get; set; }

		public MainPageViewModel()
		{
			secretColor = "FACADE";
			currentGuess = "";
			Guesses = new ObservableCollection<ColorGuess>();

			Guesses.Add( new ColorGuess("#beaded") );
            Guesses.Add( new ColorGuess("#efaced") );
        }

		[RelayCommand]
		void AddLetter(string letter)
		{
			if( CurrentGuess.Length < 6)
			{
				CurrentGuess += letter;
			}
		}

		[RelayCommand]
		void DeleteLetter() {
            if (CurrentGuess.Length > 0) {
                CurrentGuess = CurrentGuess.Remove(CurrentGuess.Length - 1, 1);
            }
        }
		[RelayCommand]
        async void Guess()
        {
			// if correct, then go to game over (DidWin=true)
			if (CurrentGuess.Length != 6) {
                await App.Current.MainPage.DisplayAlert("Alert", "Please enter 6 letters!", "OK");
                return;
			}

			if (CurrentGuess == SecretColor)
			{
				DidWin = true;
                Shell.Current.GoToAsync($"{nameof(GameOverPage)}?DidWin={DidWin}");
            }

            // else if this is the 6th guess (and it's wrong)
            // then go to game over (DidWin=false)

            // Add this guess to the Guesses
            Guesses.Add( new ColorGuess(CurrentGuess));

		}


	}
}

