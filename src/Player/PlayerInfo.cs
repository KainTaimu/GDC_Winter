namespace Game.Player;

public partial class PlayerInfo : Control
{
	[Export]
	private PlayerMovementController _movementController;

	[Export]
	private Label _currentState;

	public override void _Ready()
	{
		_movementController.OnStateChange += UpdateLabel;
	}

	private void UpdateLabel(State newState)
	{
		_currentState.Text = $"CurrentState: {newState.Name}";
	}
}
