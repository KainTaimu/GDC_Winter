namespace Game.Obstacles;

public partial class Obstacle : Node2D, IObstacle
{
	[Signal]
	public delegate void OnExitEventHandler();

	[Export]
	public ObstacleType Type { get; private set; }

	public virtual void Enter() { }

	public virtual void Exit(Area2D area)
	{
		Logger.LogDebug("EXIT", area.Name);
		EmitSignal(SignalName.OnExit);
	}

	public override string ToString()
	{
		return $"{Name}";
	}
}
