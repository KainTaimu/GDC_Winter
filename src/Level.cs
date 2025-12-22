namespace Game;

public partial class Level : Node
{
    [Export]
    public Marker2D GroundMarker;

    public override void _Ready()
    {
        GameWorld.Instance.CurrentLevel = this;
    }
}
