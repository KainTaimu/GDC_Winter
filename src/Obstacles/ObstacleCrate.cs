namespace Game.Obstacles;

public partial class ObstacleCrate : Obstacle
{
	public override void _Process(double delta)
	{
		Position -= new Vector2(ScrollingBackground.ScrollSpeed, 0) * (float)delta;
	}
}
