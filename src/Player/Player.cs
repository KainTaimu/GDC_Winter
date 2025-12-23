using Game.Weapons;

namespace Game.Players;

public partial class Player : Node2D, IHittable
{
    [Export]
    public StatController StatController { get; private set; }

    public override void _EnterTree()
    {
        GameWorld.Instance.MainPlayer = this;
    }

    public override void _Ready() { }

    public override void _Process(double delta) { }

    public void HandleHit(Weapon hitBy)
    {
        StatController.HandleHit(hitBy.Stats.Damage);
    }
}
