namespace Game;

public partial class ScrollingBackground : Node2D
{
	[Export]
	private bool _enabled = true;

	public static int ScrollSpeed { get; private set; } = 500;

	// TODO: Currently unnecessary
	[ExportCategory("Components")]
	[Export]
	private TileMapLayer _tileMapLeft;

	[Export]
	private TileMapLayer _tileMapRight;

	private Viewport _viewport;

	public override void _Ready()
	{
		_viewport = GetViewport();
	}

	public override void _Process(double delta)
	{
		if (!_enabled)
			return;

		if (Position.X < _viewport.GetVisibleRect().Size.X * -1)
			Position = Vector2.Zero;

		Position -= new Vector2(ScrollSpeed, 0) * (float)delta;
	}
}
