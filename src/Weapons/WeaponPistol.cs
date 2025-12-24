using System.Threading.Tasks;
using Game.Hud;

namespace Game.Weapons;

[Obsolete("Out of time")]
public partial class WeaponPistol : Weapon
{
    [Export]
    private PackedScene _projectileScene;

    public int MagazineCapacity
    {
        get => _magazineCapacity;
    }

    public int MagazineCount
    {
        get => _magazineCount;
    }

    private double _fireCooldown;
    private bool _isReloading;

    private int _reloadTimeMs = 1500;
    private float _bloomCoefficient = 0.03f;
    private int _magazineCapacity = 30;
    private int _magazineCount;

    private float _horizontalBaseRecoil = 3f;
    private float _horizontalRecoilRandom = 1f;
    private float _verticalBaseRecoil = 3f;
    private float _verticalRecoilRandom = 0.1f;

    public override void _Ready()
    {
        _magazineCapacity = (int)Stats.Additional["MagazineCapacity"];
        _magazineCount = _magazineCapacity;
        _reloadTimeMs = (int)Stats.Additional["ReloadTimeMs"];
        _bloomCoefficient = (float)Stats.Additional["BloomCoefficient"];

        _horizontalBaseRecoil = (float)Stats.Additional["HorizontalBaseRecoil"];
        _horizontalRecoilRandom = (float)Stats.Additional["HorizontalRecoilRandom"];
        _verticalBaseRecoil = (float)Stats.Additional["VerticalBaseRecoil"];
        _verticalRecoilRandom = (float)Stats.Additional["VerticalRecoilRandom"];
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventKey)
            return;

        if (Input.IsActionPressed("reload") && !_isReloading)
            Reload();
    }

    public override void _Process(double delta)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (_isReloading)
                return;

            if (_magazineCount <= 0)
            {
                Reload();
                return;
            }

            if (_fireCooldown > 0)
                return;

            Attack();
            _fireCooldown = Stats.AttackSpeed;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_fireCooldown <= 0)
            return;
        _fireCooldown -= delta;
    }

    public void Attack()
    {
        if (_magazineCount <= 0 || _isReloading)
            return;

        _magazineCount--;

        var crosshair = Crosshair.Instance;
        var playerVector = GetCanvasTransform() * Position;
        var mouseVector =
            crosshair.CrosshairSprite.GetCanvasTransform()
            * crosshair.CrosshairSprite.GlobalPosition;
        var rotation = playerVector.AngleToPoint(mouseVector);

        var bloom = (float)GD.Randfn(rotation, _bloomCoefficient);

        var projectile = _projectileScene.Instantiate<ProjectileBullet>();
        projectile.Initialize(this);
        projectile.SetPosition(Position);
        projectile.SetRotation(bloom);
        projectile.TargetsWhat = ProjectileTargetsWhat.Obstacle;
        projectile.InitialDistance = GlobalPosition.DistanceTo(
            crosshair.CrosshairSprite.GlobalPosition
        );
        AddChild(projectile);

        ApplyCursorRecoil();
    }

    public void Reload()
    {
        if (_magazineCount == _magazineCapacity)
            return;

        _isReloading = true;
        _ = ReloadTask();
    }

    private async Task ReloadTask()
    {
        await Task.Delay(_reloadTimeMs);
        _isReloading = false;
        _magazineCount = _magazineCapacity;
    }

    private void ApplyCursorRecoil()
    {
        var recoilX = _horizontalBaseRecoil * (float)GD.Randfn(0, _horizontalRecoilRandom);
        var recoilY = _verticalBaseRecoil * (float)GD.Randfn(1, _verticalRecoilRandom);
        recoilY = Math.Clamp(recoilY, 2, float.MaxValue);

        var recoil = new Vector2(recoilX, -recoilY);
        Crosshair.Instance.Recoil.ApplyImpulse(recoil, 1f);
    }
}
