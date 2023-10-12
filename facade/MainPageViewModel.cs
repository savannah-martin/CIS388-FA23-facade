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

		[ObservableProperty]
		public Color secretColorBkgd;

        public ObservableCollection<ColorGuess> Guesses { get; set; }

		//public string SecretColor { get; set; }

		private string randomColor()
		{

			string letters = "ABCDEF";

			string color = "";
			Random rand = new Random();
			for ( int i =0; i<6; i++) {
				color += letters[rand.Next(0, 6)];
			}

			return color;
		}

		public MainPageViewModel()
		{
			secretColor = randomColor();
            SecretColorBkgd = Color.FromArgb('#' + secretColor);
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
                string currentAnswer = SecretColor;
                SecretColor = randomColor();
				SecretColorBkgd = Color.FromArgb('#' + SecretColor);
                await Shell.Current.GoToAsync($"{nameof(GameOverPage)}?DidWin={DidWin}&Attempts={Attempts}&Answer={currentAnswer}");
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
                    DidWin = false;
					string currentAnswer = SecretColor;
                    SecretColor = randomColor();
                    await Shell.Current.GoToAsync($"{nameof(GameOverPage)}?DidWin={DidWin}&Attempts={Attempts}&Answer={currentAnswer}");
                    SecretColorBkgd = Color.FromArgb('#' + SecretColor);
                    Attempts = 0;
                }
			}
			// reset guess string
			CurrentGuess = "";
		}


	}
}

