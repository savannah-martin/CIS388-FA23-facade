namespace facade;

[QueryProperty("DidWin", "DidWin")]
[QueryProperty("Attempts", "Attempts")]
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
    private string attempts;
    public string Attempts
    {
        get => attempts;
        set
        {
            attempts = value;
            AttemptsLabel.Text = attempts;
            Console.WriteLine(attempts);
            Console.WriteLine(AttemptsLabel);
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
