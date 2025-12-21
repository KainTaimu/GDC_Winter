namespace Game.Player;

public partial class Player : Node2D
{
    [Export]
    public StatController StatController { get; private set; }

    public override void _Ready() { }

    public override void _Process(double delta) { }
}
