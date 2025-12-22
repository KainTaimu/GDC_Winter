namespace Game.Player;

public partial class PlayerInfo : Control
{
	[Export]
	private PlayerMovementController _movementController;

	[Export]
	private Label _currentState;

	public override void _Process(double delta)
	{
		_currentState.Text = $"CurrentState: {_movementController.CurrentState.Name}";
	}
}
