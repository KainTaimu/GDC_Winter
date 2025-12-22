using System.Linq;
using Game.Obstacles;

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

	private bool _hasCollided;

	public override void Enter()
	{
		_collisionArea.AreaEntered += OnObstacleCollision;
	}

	public override void Exit()
	{
		_collisionArea.AreaEntered -= OnObstacleCollision;
	}

	public override void _Ready() { }

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
		switch (interactionArea.Obstacle)
		{
			case ObstacleCrate:
				MovementController.ChangeState<StateJumpingOverBox>();
				break;
			default:
				Logger.LogDebug(interactionArea.Obstacle.ToString());
				break;
		}
	}

	private void OnObstacleCollision(Area2D collision)
	{
		Logger.LogDebug("Collided with", collision.Name);
		MovementController.ChangeState<StateCollidedWithBox>();
	}
}
