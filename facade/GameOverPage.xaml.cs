namespace facade;

[QueryProperty("DidWin", "DidWin")]
public partial class GameOverPage : ContentPage
{
	private bool didWin;
	public bool DidWin
	{
		get => didWin;
		set
		{
			didWin = value;
			if (didWin)
			{
				ResultLabel.Text = "YOU WON!";
			}
			else
			{
				ResultLabel.Text = "YOU LOST!";
			}
		}
	}

    //private string result;
    //public string Result {
    //	get => result;
    //	set
    //	{
    //		result = value;
    //           ResultLabel.Text = "You " + result;
    //       }
    //}

    async void New_Game(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    public GameOverPage()
	{
		InitializeComponent();
	}
}
