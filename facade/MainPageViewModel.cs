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
        private int attempts;

		[ObservableProperty]
        private bool didWin;

        public ObservableCollection<ColorGuess> Guesses { get; set; }

		//public string SecretColor { get; set; }

		public MainPageViewModel()
		{
			secretColor = "BEEFED";
			currentGuess = "";
			attempts = 0;
			Guesses = new ObservableCollection<ColorGuess>();

			//Guesses.Add( new ColorGuess("#beaded") );
            //Guesses.Add( new ColorGuess("#efaced") );
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
			// if not long enough, alert
			if (CurrentGuess.Length != 6) {
                await App.Current.MainPage.DisplayAlert("Alert", "Please enter 6 letters!", "OK");
                return;
			}

			// increment guess number
			Attempts += 1;

            // if correct, then go to game over (DidWin=true)
            if (CurrentGuess == SecretColor)
			{
                DidWin = true;
                await Shell.Current.GoToAsync($"{nameof(GameOverPage)}?DidWin={DidWin}&Attempts={Attempts}");
                Guesses.Clear();
				Attempts = 0;
            }

            else
			{
				// if already guessed, alert
				//if (Guesses.Contains(new ColorGuess($"#{CurrentGuess}")))
				//{
					//await App.Current.MainPage.DisplayAlert("Alert", "You already guessed that silly goose.", "OK");
					//return;
				//}

				// else, add
				Guesses.Add(new ColorGuess($"#{CurrentGuess}"));
				// else if this is the 6th guess (and it's wrong)
				// then go to game over (DidWin=false)
				if (Guesses.Count == 6)
				{
                    Guesses.Clear();
					Attempts = 0;
                    DidWin = false;
					await Shell.Current.GoToAsync($"{nameof(GameOverPage)}?DidWin={DidWin}&Attempts={Attempts}");
				}
			}
			// reset guess string
			CurrentGuess = "";
		}


	}
}

