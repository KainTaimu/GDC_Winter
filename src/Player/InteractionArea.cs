using Game.Obstacles;

namespace Game.Player;

public partial class InteractionArea : Area2D
{
    [Export]
    public Obstacle Obstacle { get; private set; }
}
