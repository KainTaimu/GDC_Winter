namespace Game.Weapons;

public partial class ProjectileBullet : Projectile
{
    public int ProjectileSpeed;

    public override void _Process(double delta)
    {
        MoveTowardPoint(delta);
    }

    private void MoveTowardPoint(double delta)
    {
        var moveVector = new Vector2(1, 0).Rotated(Rotation) * ProjectileSpeed * (float)delta;

        Position += moveVector;
    }
}
