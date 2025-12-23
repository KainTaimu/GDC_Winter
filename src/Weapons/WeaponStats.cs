namespace Game.Weapons;

[GlobalClass]
public partial class WeaponStats : Resource
{
    [Export]
    public int Damage = 1;

    [Export]
    public int ProjectileSpeed = 1200;

    [Export]
    public float AttackSpeed = 1;
}
