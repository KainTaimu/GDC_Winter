namespace Game.Players;

public partial class StateCollidedWithBox : State
{
    [Export]
    private Area2D _collisionArea;

    [Export]
    private StatController _statController;

    public override void Enter()
    {
        _statController.HandleHit(1); // TODO: Custom damage by obstacle type.
    }

    public override void Exit() { }

    public override void _Ready() { }

    private void AfterCollisionWithBox(Area2D area)
    {
        MovementController.ChangeState<StateGrounded>();
    }
}
