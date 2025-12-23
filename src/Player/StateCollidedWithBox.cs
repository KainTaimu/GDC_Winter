namespace Game.Player;

public partial class StateCollidedWithBox : State
{
    [Export]
    private Area2D _collisionArea;

    [Export]
    private StatController _statController;

    public override void Enter()
    {
        _statController.Stats.Health -= 1;
        if (_statController.Stats.Health <= 0)
        {
            MovementController.ChangeState<StateDying>();
        }
        else
        {
            // _collisionArea.AreaExited += AfterCollisionWithBox;
        }
    }

    public override void Exit() { }

    public override void _Ready() { }

    private void AfterCollisionWithBox(Area2D area)
    {
        MovementController.ChangeState<StateGrounded>();
    }
}
