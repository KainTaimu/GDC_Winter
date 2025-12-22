using System.Linq;

namespace Game.Player;

public partial class StateGrounded : State
{
	[Export]
	private Node2D _owner;

	[Export]
	private StatController _statController;

	[Export]
	private Area2D _interactionArea;

	[Export]
	private Area2D _collisionArea;

	public override void Enter() { }

	public override void Exit() { }

	public override void _Ready()
	{
		_collisionArea.AreaEntered += OnObstacleCollision;
	}

	public override void Process(double delta)
	{
		if (!Input.IsActionJustPressed(InputMapNames.MoveUp))
			return;

		var interactable = _interactionArea.GetOverlappingAreas();
		if (interactable.Count == 0)
			return;

		var interactionArea = interactable.OfType<InteractionArea>().FirstOrDefault() ?? null;
		if (interactionArea is null)
			return;

		Logger.LogDebug("Interacted", interactionArea?.Name);
	}

	private void OnObstacleCollision(Area2D collision)
	{
		Logger.LogDebug("Collided with", collision.Name);
	}
}
