namespace Game.Weapons;

public partial class ProjectileBullet : Projectile
{
    public float InitialDistance;

    public override void Initialize(Weapon origin)
    {
        base.Initialize(origin);
    }

    public override void _Process(double delta)
    {
        MoveTowardPoint(delta);
    }

    public override void _Ready()
    {
        base._Ready();
        var tweenScale = CreateTween()
            .BindNode(this)
            .SetTrans(Tween.TransitionType.Linear)
            .SetEase(Tween.EaseType.In);

        tweenScale.TweenProperty(this, "scale", new Vector2(3, 1), 0.13f);
        InitialDistance = (int)
            _origin.GlobalPosition.DistanceTo(GameWorld.Instance.MainPlayer.GlobalPosition);
    }

    private void MoveTowardPoint(double delta)
    {
        var d = InitialDistance;
        var t = 0.5f;
        var v = d / t;
        var moveVector = new Vector2(1, 0).Rotated(Rotation) * v * (float)delta;
        moveVector *= -1;

        Position += moveVector;
    }

    private void OnExitScreen()
    {
        QueueFree();
    }
}
