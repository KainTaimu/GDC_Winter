namespace Game.Player;

public partial class StateJumpingOverBox : State
{
    [Export]
    private Area2D _interactionArea;

    public override void Enter() { }

    public override void Exit() { }

    public override void _Ready()
    {
        _interactionArea.AreaExited += OnLeavingInteractionArea;
    }

    private void OnLeavingInteractionArea(Area2D area)
    {
        MovementController.ChangeState<StateGrounded>();
    }
}
