namespace Game.Obstacles;

public partial class ObstacleCrate : Obstacle
{
	[Export]
	public Marker2D Top { get; private set; }

	public override void _Process(double delta)
	{
		Position -= new Vector2(ScrollingBackground.ScrollSpeed, 0) * (float)delta;
	}
}
