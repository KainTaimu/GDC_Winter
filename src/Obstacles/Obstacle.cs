namespace Game.Obstacles;

public partial class Obstacle : Node2D, IObstacle
{
    [Export]
    public ObstacleType Type { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public enum ObstacleType
    {
        Ground,
        Roof,
    }
}
