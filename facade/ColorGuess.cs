using System;
namespace facade
{
	public class ColorGuess
	{
		public string Guess { get; set; }

		public Color Color => Color.FromArgb(Guess);

		public ColorGuess(string color)
		{
			Guess = color;
		}
	}
}

