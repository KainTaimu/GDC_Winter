using Game.Players;

namespace Game;

public partial class ScoreController : Node
{
	[Export]
	private Player _player;

	[ExportCategory("Components")]
	[Export]
	private Label _scoreLabel;

	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			_scoreLabel.Text = "Score: " + value.ToString();
		}
	}

	private int _score;

	public static ScoreController Instance;

	public ScoreController()
	{
		if (Instance is not null)
		{
			Logger.LogError("Cannot have multiple instances of a singleton!");
			QueueFree();
			return;
		}

		Instance = this;
	}

	public override void _Ready() { }

	public override void _Process(double delta) { }
}
