namespace Game.Player;

public partial class StateRunning : State
{
    [Export]
    private Node2D _owner;

    [Export]
    private StatController _statController;

    public override void Enter() { }

    public override void Exit() { }

    public override void Transition(IState previousState)
    {
        switch (previousState)
        {
            case StateJumpBox:
                break;
            default:
                break;
        }
    }

    public override void Process(double delta)
    {
        var moveX = _statController.Stats.MoveSpeed * delta;
        _owner.Position = new Vector2(_owner.Position.X + (float)moveX, _owner.Position.Y);
    }

    private void TransitionToJumpBox(StateJumpBox state) { }
}
