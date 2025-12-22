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
    }

    public override void Exit() { }

    public override void _Ready()
    {
        _collisionArea.AreaExited += AfterCollisionWithBox;
    }

    private void AfterCollisionWithBox(Area2D area)
    {
        MovementController.ChangeState<StateGrounded>();
    }
}
