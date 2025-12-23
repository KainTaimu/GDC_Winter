namespace Game.Player;

public partial class StateDying : State
{
    public override void Enter()
    {
        GameWorld.Instance.CurrentLevel.ScrollingBackground.Stop();
        MovementController.Lock(this);
        GameWorld.Instance.EmitSignal(GameWorld.SignalName.OnPlayerDeath);
    }

    public override void Exit()
    {
        MovementController.Unlock(this);
    }
}
