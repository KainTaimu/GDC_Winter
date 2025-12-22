namespace Game.Player;

public partial class StatController : Node
{
    [Export]
    public Stats Stats { get; private set; }

    public override void _Ready()
    {
        Stats.Initialize();
    }
}
