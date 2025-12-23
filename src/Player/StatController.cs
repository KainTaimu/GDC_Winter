namespace Game.Players;

public partial class StatController : Node
{
    [Export]
    public Stats Stats { get; private set; }

    [ExportCategory("Components")]
    [Export]
    private PlayerMovementController _movementController;

    public override void _Ready()
    {
        Stats.Initialize();
    }

    public void HandleHit(int damage)
    {
        if (Stats.Health == 0)
            return;

        if (Stats.Health - damage == 0)
        {
            Stats.Health = 0;
            _movementController.ChangeState<StateDying>();
            GameWorld.Instance.AnnouncePlayerDead(this);
        }
        else
        {
            Stats.Health -= damage;
        }
    }
}
