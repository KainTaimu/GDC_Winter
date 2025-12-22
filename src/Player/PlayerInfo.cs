namespace Game.Player;

public partial class PlayerInfo : Control
{
    [Export]
    private PlayerMovementController _movementController;

    [Export]
    private StatController _statController;

    [Export]
    private Label _health;

    [Export]
    private Label _currentState;

    public override void _Ready()
    {
        _movementController.OnStateChange += UpdateLabel;
        UpdateLabel(_movementController.CurrentState);
    }

    private void UpdateLabel(State newState)
    {
        _health.Text = $"Health: {_statController.Stats.Health}";
        _currentState.Text = $"CurrentState: {newState.Name}";
    }
}
